using ClickerClass.Buffs;
using ClickerClass.Items;
using ClickerClass.Items.Armors;
using ClickerClass.Items.Tools;
using ClickerClass.NPCs;
using ClickerClass.Projectiles;
using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ClickerClass
{
	public partial class ClickerPlayer : ModPlayer
	{
		//Key presses
		public double pressedAutoClick;
		public int clickerClassTime = 0;

		//-Clicker-
		//Misc
		public Color clickerColor = new Color(0, 0, 0, 0);
		/// <summary>
		/// Visual indicator that the cursor is inside clicker radius
		/// </summary>
		public bool clickerInRange = false;
		/// <summary>
		/// Visual indicator that the cursor is inside Motherboard radius
		/// </summary>
		public bool clickerInRangeMotherboard = false;
		public bool GlowVisual => clickerInRange || clickerInRangeMotherboard;
		public bool clickerSelected = false;
		/// <summary>
		/// False if phase reach
		/// </summary>
		public bool clickerDrawRadius = false;
		public bool clickerAutoClick = false;
		public int clickerPerSecondTimer = 0;
		public int clickerPerSecond = 0;
		/// <summary>
		/// Saved amount of clicks done with any clicker, accumulated, fluff
		/// </summary>
		public int clickerTotal = 0;
		/// <summary>
		/// Amount of clicks done, constantly incremented
		/// </summary>
		public int clickAmount = 0;

		//Out of combat
		public bool outOfCombat = true;
		public int outOfCombatTimer = 0;

		//Armor
		public int setAbilityDelayTimer = 0;
		public float setMotherboardRatio = 0f;
		public float setMotherboardAngle = 0f;
		/// <summary>
		/// Calculated after clickerRadius is calculated, and if the Motherboard set is worn
		/// </summary>
		public Vector2 setMotherboardPosition = Vector2.Zero;
		public float setMotherboardAlpha = 0f;
		public int setMotherboardFrame = 0;
		public bool setMotherboardFrameShift = false;
		public bool setMotherboard = false;
		public bool SetMotherboardDraw => setMotherboard && setMotherboardRatio > 0;


		public bool setMiceAllowed = true;
		public bool setMice = false;
		public bool SetMiceDraw => setMice && setMiceAllowed;

		public bool setPrecursorAllowed = true;
		public bool setPrecursor = false;
		public bool SetPrecursorDraw => setPrecursor && setPrecursorAllowed;

		public bool setOverclockAllowed = true;
		public bool setOverclock = false;
		public bool SetOverclockDraw => setOverclock && setOverclockAllowed;

		//Acc
		public bool accChocolateChip = false;
		public bool accEnchantedLED = false;
		public bool accEnchantedLED2 = false; //different visuals
		public bool accHandCream = false;
		public bool accStickyKeychain = false;
		public bool accGlassOfMilk = false;
		public bool accCookie = false;
		public bool accCookie2 = false; //different visuals
		public bool accClickingGlove = false;
		public bool accAncientClickingGlove = false;
		public bool accRegalClickingGlove = false;
		public bool accPortableParticleAccelerator = false; //add to api when implemented

		public int accClickingGloveTimer = 0;
		public int accCookieTimer = 0;

		//Stats
		/// <summary>
		/// Click crit 
		/// </summary>
		public int clickerCrit = 4;

		/// <summary>
		/// Click damage add flat
		/// </summary>
		public int clickerDamageFlat = 0;

		/// <summary>
		/// Click damage add multiplier
		/// </summary>
		public float clickerDamage = 1f;

		/// <summary>
		/// How many less clicks are required to trigger an effect
		/// </summary>
		public int clickerBonus = 0;

		/// <summary>
		/// Multiplier to clicks required to trigger an effect
		/// </summary>
		public float clickerBonusPercent = 1f;

		/// <summary>
		/// Effective clicker radius in pixels when multiplied by 100
		/// </summary>
		public float clickerRadius = 1f;

		/// <summary>
		/// Clicker radius in pixels
		/// </summary>
		public float ClickerRadiusReal => clickerRadius * 100;

		/// <summary>
		/// Motherboard radius in pixels
		/// </summary>
		public float ClickerRadiusMotherboard => ClickerRadiusReal * 0.5f;

		//Helper methods
		/// <summary>
		/// Returns the position from the ratio and angle
		/// </summary>
		public Vector2 CalculateMotherboardPosition()
		{
			float length = setMotherboardRatio * ClickerRadiusReal;
			Vector2 direction = setMotherboardAngle.ToRotationVector2();
			return direction * length;
		}

		/// <summary>
		/// Construct ratio and angle from position
		/// </summary>
		public void SetMotherboardRelativePosition(Vector2 position)
		{
			Vector2 toPosition = position - Player.Center;
			float length = toPosition.Length();
			float radius = ClickerRadiusReal;
			float ratio = length / radius;
			setMotherboardRatio = ratio;
			setMotherboardAngle = toPosition.ToRotation();
		}

		internal int originalSelectedItem;
		internal bool autoRevertSelectedItem = false;

		/// <summary>
		/// Uses the item in the specified index from the players inventory
		/// </summary>
		public void QuickUseItemInSlot(int index)
		{
			if (index > -1 && index < Main.InventorySlotsTotal && Player.inventory[index].type != ItemID.None)
			{
				if (Player.CheckMana(Player.inventory[index], -1, false, false))
				{
					originalSelectedItem = Player.selectedItem;
					autoRevertSelectedItem = true;
					Player.selectedItem = index;
					Player.controlUseItem = true;
					Player.ItemCheck(Player.whoAmI);
				}
				else
				{
					SoundEngine.PlaySound(SoundID.Drip, (int)Player.Center.X, (int)Player.Center.Y, Main.rand.Next(3));
				}
			}
		}

		/// <summary>
		/// Returns the amount of clicks required for an effect to trigger. Includes various bonuses
		/// </summary>
		public int GetClickAmountTotal(ClickerItemCore clickerItem)
		{
			//Doesn't go below 1
			return Math.Max(1, (int)((clickerItem.itemClickerAmount + clickerItem.clickBoostPrefix - clickerBonus) * clickerBonusPercent));
		}

		/// <summary>
		/// Returns the amount of clicks required for an effect to trigger. Includes various bonuses
		/// </summary>
		public int GetClickAmountTotal(Item item)
		{
			return GetClickAmountTotal(item.GetGlobalItem<ClickerItemCore>());
		}

		public override void ResetEffects()
		{
			//-Clicker-
			//Misc
			clickerColor = new Color(0, 0, 0, 0);
			clickerInRange = false;
			clickerInRangeMotherboard = false;
			clickerSelected = false;
			clickerDrawRadius = false;

			//Armor
			setMiceAllowed = true;
			setMotherboard = false;
			setMiceAllowed = true;
			setMice = false;
			setPrecursorAllowed = true;
			setPrecursor = false;
			setOverclockAllowed = true;
			setOverclock = false;

			//Acc
			accChocolateChip = false;
			accEnchantedLED = false;
			accEnchantedLED2 = false;
			accStickyKeychain = false;
			accHandCream = false;
			accGlassOfMilk = false;
			accCookie = false;
			accCookie2 = false;
			accClickingGlove = false;
			accAncientClickingGlove = false;
			accRegalClickingGlove = false;
			accPortableParticleAccelerator = false;

			//Stats
			clickerCrit = 4;
			clickerDamageFlat = 0;
			clickerDamage = 1f;
			clickerBonus = 0;
			clickerBonusPercent = 1f;
			clickerRadius = 1f;
		}

		public override void Initialize()
		{
			clickerTotal = 0;
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"clickerTotal", clickerTotal}
			};
		}

		public override void Load(TagCompound tag)
		{
			clickerTotal = tag.GetInt("clickerTotal");
		}

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			// checks for frozen, webbed and stoned
			if (Player.CCed)
			{
				return;
			}

			if (ClickerClass.AutoClickKey.JustPressed)
			{
				if (Math.Abs(clickerClassTime - pressedAutoClick) > 60)
				{
					pressedAutoClick = clickerClassTime;

					SoundEngine.PlaySound(SoundID.MenuTick, Player.position);
					clickerAutoClick = clickerAutoClick ? false : true;
				}
			}
		}

		public override void PreUpdate()
		{
			if (Player.whoAmI == Main.myPlayer)
			{
				if (autoRevertSelectedItem)
				{
					if (Player.itemTime == 0 && Player.itemAnimation == 0)
					{
						Player.selectedItem = originalSelectedItem;
						autoRevertSelectedItem = false;
					}
				}
			}

			if (Player.whoAmI == Main.myPlayer)
			{
				if (Player.itemTime == 0 && Player.itemAnimation == 0)
				{
					if (accRegalClickingGlove && accClickingGloveTimer > 30)
					{
						QuickUseItemInSlot(Player.selectedItem);
						accClickingGloveTimer = 0;
					}
					else if (accAncientClickingGlove && accClickingGloveTimer > 60)
					{
						QuickUseItemInSlot(Player.selectedItem);
						accClickingGloveTimer = 0;
					}
					else if (accClickingGlove && accClickingGloveTimer > 180)
					{
						QuickUseItemInSlot(Player.selectedItem);
						accClickingGloveTimer = 0;
					}
				}
			}
		}

		public override void PostUpdateEquips()
		{
			clickerClassTime++;
			if (clickerClassTime > 36000)
			{
				clickerClassTime = 0;
			}

			if (!accHandCream)
			{
				clickerAutoClick = false;
			}

			if (setAbilityDelayTimer > 0)
			{
				setAbilityDelayTimer--;
			}

			if (!setMotherboard)
			{
				setMotherboardPosition = Vector2.Zero;
				setMotherboardRatio = 0f;
				setMotherboardAngle = 0f;
			}
			else
			{
				setMotherboardAlpha += !setMotherboardFrameShift ? 0.025f : -0.025f;
				if (setMotherboardAlpha >= 1f)
				{
					setMotherboardFrameShift = true;
				}

				if (setMotherboardFrameShift && setMotherboardAlpha <= 0.25f)
				{
					setMotherboardFrame++;
					if (setMotherboardFrame >= 4)
					{
						setMotherboardFrame = 0;
					}
					setMotherboardFrameShift = false;
				}
			}

			if (ClickerSystem.IsClickerWeapon(Player.HeldItem, out ClickerItemCore clickerItem))
			{
				clickerSelected = true;
				clickerDrawRadius = true;
				if (clickerItem.itemClickerEffect.Contains("Phase Reach"))
				{
					clickerDrawRadius = false;
				}

				if (clickerItem.radiusBoost > 0f)
				{
					clickerRadius += clickerItem.radiusBoost;
				}

				if (clickerItem.radiusBoostPrefix > 0f)
				{
					clickerRadius += clickerItem.radiusBoostPrefix;
				}

				//collision
				if (Vector2.Distance(Main.MouseWorld, Player.Center) < ClickerRadiusReal && Collision.CanHit(new Vector2(Player.Center.X, Player.Center.Y - 12), 1, 1, Main.MouseWorld, 1, 1))
				{
					clickerInRange = true;
				}
				if (setMotherboard)
				{
					//Important: has to be after final clickerRadius calculation because it depends on it
					setMotherboardPosition = Player.Center + CalculateMotherboardPosition();
				}

				//collision
				if (Vector2.Distance(Main.MouseWorld, setMotherboardPosition) < ClickerRadiusMotherboard && Collision.CanHit(setMotherboardPosition, 1, 1, Main.MouseWorld, 1, 1))
				{
					clickerInRangeMotherboard = true;
				}
				clickerColor = clickerItem.clickerColorItem;

				//Glove acc
				if (!outOfCombat && (accClickingGlove || accAncientClickingGlove || accRegalClickingGlove))
				{
					accClickingGloveTimer++;
				}
				else
				{
					accClickingGloveTimer = 0;
				}
			}

			if (Player.HasBuff(ModContent.BuffType<Haste>()))
			{
				Player.armorEffectDrawShadow = true;
			}

			//Armor
			int head = 0;
			int body = 1;
			int legs = 2;
			int vanityHead = 10;
			int vanityBody = 11;
			int vanityLegs = 12;

			Item itemHead = Player.armor[head];
			Item itemBody = Player.armor[body];
			Item itemLegs = Player.armor[legs];

			Item itemVanityHead = Player.armor[vanityHead];
			Item itemVanityBody = Player.armor[vanityBody];
			Item itemVanityLegs = Player.armor[vanityLegs];

			if (Player.wereWolf || Player.merman)
			{
				setMiceAllowed = false;
				setPrecursorAllowed = false;
				setOverclockAllowed = false;
			}

			if (itemVanityHead.type > 0)
			{
				if (itemVanityHead.type != ModContent.ItemType<MiceMask>())
				{
					setMiceAllowed = false;
				}
				if (itemVanityHead.type != ModContent.ItemType<PrecursorHelmet>())
				{
					setPrecursorAllowed = false;
				}
				if (itemVanityHead.type != ModContent.ItemType<OverclockHelmet>())
				{
					setOverclockAllowed = false;
				}
			}
			if (itemVanityBody.type > 0)
			{
				if (itemVanityBody.type != ModContent.ItemType<MiceSuit>())
				{
					setMiceAllowed = false;
				}
				if (itemVanityBody.type != ModContent.ItemType<PrecursorBreastplate>())
				{
					setPrecursorAllowed = false;
				}
				if (itemVanityBody.type != ModContent.ItemType<OverclockSuit>())
				{
					setOverclockAllowed = false;
				}
			}
			if (itemVanityLegs.type > 0)
			{
				if (itemVanityLegs.type != ModContent.ItemType<MiceBoots>())
				{
					setMiceAllowed = false;
				}
				if (itemVanityLegs.type != ModContent.ItemType<PrecursorGreaves>())
				{
					setPrecursorAllowed = false;
				}
				if (itemVanityLegs.type != ModContent.ItemType<OverclockBoots>())
				{
					setOverclockAllowed = false;
				}
			}

			if (SetOverclockDraw)
			{
				Lighting.AddLight(Player.position, 0.3f, 0.075f, 0.075f);
			}
			if (SetPrecursorDraw)
			{
				Lighting.AddLight(Player.position, 0.2f, 0.15f, 0.05f);
			}
			if (SetMiceDraw)
			{
				Lighting.AddLight(Player.position, 0.1f, 0.1f, 0.3f);
			}

			//Acc
			//Cookie acc
			if ((accCookie || accCookie2) && clickerSelected)
			{
				accCookieTimer++;
				if (Player.whoAmI == Main.myPlayer && accCookieTimer > 600)
				{
					int radius = (int)(95 * clickerRadius);
					if (radius > 350)
					{
						radius = 350;
					}

					//Circles give me a damn headache...
					double r = radius * Math.Sqrt(Main.rand.NextFloat(0f, 1f));
					double theta = Main.rand.NextFloat(0f, 1f) * 2 * 3.14;
					double xOffset = Player.Center.X + r * Math.Cos(theta);
					double yOffset = Player.Center.Y + r * Math.Sin(theta);

					if (accCookie2 && Main.rand.NextFloat() <= 0.1f)
					{
						Projectile.NewProjectile((float)(xOffset), (float)(yOffset), 0f, 0f, ModContent.ProjectileType<CookiePro>(), 0, 0f, Player.whoAmI, 1f);
					}
					else
					{
						Projectile.NewProjectile((float)(xOffset), (float)(yOffset), 0f, 0f, ModContent.ProjectileType<CookiePro>(), 0, 0f, Player.whoAmI);
					}

					accCookieTimer = 0;
				}

				//Cookie Click
				if (Player.whoAmI == Main.myPlayer)
				{
					for (int i = 0; i < 1000; i++)
					{
						Projectile cookieProjectile = Main.projectile[i];

						if (cookieProjectile.active && cookieProjectile.type == ModContent.ProjectileType<CookiePro>() && cookieProjectile.owner == Player.whoAmI)
						{
							if (Main.mouseLeft && Main.mouseLeftRelease && Vector2.Distance(cookieProjectile.Center, Main.MouseWorld) < 30)
							{
								if (cookieProjectile.ai[0] == 1f)
								{
									SoundEngine.PlaySound(2, (int)Player.position.X, (int)Player.position.Y, 4);
									Player.AddBuff(ModContent.BuffType<CookieBuff>(), 600);
									Player.HealLife(10);
									for (int k = 0; k < 10; k++)
									{
										Dust dust = Dust.NewDustDirect(cookieProjectile.Center, 20, 20, 87, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.15f);
										dust.noGravity = true;
									}
								}
								else
								{
									SoundEngine.PlaySound(2, (int)Player.position.X, (int)Player.position.Y, 2);
									Player.AddBuff(ModContent.BuffType<CookieBuff>(), 300);
									for (int k = 0; k < 10; k++)
									{
										Dust dust = Dust.NewDustDirect(cookieProjectile.Center, 20, 20, 0, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 75, default, 1.5f);
										dust.noGravity = true;
									}
								}
								cookieProjectile.Kill();
							}
						}
					}
				}
			}

			//Milk acc
			if (accGlassOfMilk)
			{
				float bonusDamage = (float)(clickerPerSecond + 0.015f);
				if (bonusDamage >= 0.15f)
				{
					bonusDamage = 0.15f;
				}
				clickerDamage += bonusDamage;

				clickerPerSecondTimer++;
				if (clickerPerSecondTimer > 60)
				{
					clickerPerSecond = 0;
					clickerPerSecondTimer = 0;
				}
			}
			else
			{
				clickerPerSecondTimer = 0;
				clickerPerSecond = 0;
			}

			// Out of Combat timer
			if (outOfCombatTimer > 0)
			{
				outOfCombatTimer--;
				outOfCombat = false;
			}
			else
			{
				outOfCombat = true;
			}
		}

		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (ClickerSystem.IsClickerWeapon(Player.HeldItem))
			{
				if (target.GetGlobalNPC<ClickerGlobalNPC>().embrittle)
				{
					damage += 8;
				}
			}
		}

		public override void OnHitNPCWithProj(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			outOfCombatTimer = 300;
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			outOfCombatTimer = 300;
		}

		public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			outOfCombatTimer = 300;
		}

		/*
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			int index = layers.IndexOf(PlayerLayer.HeldItem);
			if (index != -1)
			{
				layers.Insert(index + 1, WeaponGlow);
			}
			index = layers.IndexOf(PlayerLayer.Head);
			if (index != -1)
			{
				layers.Insert(index + 1, HeadGlow);
			}
			index = layers.IndexOf(PlayerLayer.Legs);
			if (index != -1)
			{
				layers.Insert(index + 1, LegsGlow);
			}
			index = layers.IndexOf(PlayerLayer.Body);
			if (index != -1)
			{
				layers.Insert(index + 1, BodyGlow);
			}
			index = layers.IndexOf(PlayerLayer.Arms);
			if (index != -1)
			{
				layers.Insert(index + 1, ArmsGlow);
			}
			index = layers.IndexOf(PlayerLayer.MiscEffectsFront);
			if (index != -1)
			{
				layers.Insert(index + 1, MiscEffects);
			}

			WeaponGlow.visible = true;
			HeadGlow.visible = true;
			LegsGlow.visible = true;
			ArmsGlow.visible = true;
			MiscEffects.visible = true;
		}

		//Head
		public static readonly PlayerLayer HeadGlow = new PlayerLayer("ClickerClass", "HeadGlow", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;

			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ClickerClass");

			if (modPlayer.SetMiceDraw)
			{
				texture = mod.GetTexture("Glowmasks/MiceMask_Glow");
			}
			if (modPlayer.SetPrecursorDraw)
			{
				texture = mod.GetTexture("Glowmasks/PrecursorHelmet_Glow");
				color *= 0.5f;
			}
			if (modPlayer.SetOverclockDraw)
			{
				texture = mod.GetTexture("Glowmasks/OverclockHelmet_Glow");
				color *= 0.75f;
			}

			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.headOrigin, drawPlayer.bodyFrame, color, drawPlayer.headRotation, drawInfo.headOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.headArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});

		//Body
		public static readonly PlayerLayer BodyGlow = new PlayerLayer("ClickerClass", "BodyGlow", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;

			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ClickerClass");

			if (modPlayer.SetMiceDraw)
			{
				if (drawPlayer.Male)
				{
					texture = mod.GetTexture("Glowmasks/MiceSuit_Glow");
				}
				else
				{
					texture = mod.GetTexture("Glowmasks/MiceSuitFemale_Glow");
				}
			}
			if (modPlayer.SetPrecursorDraw)
			{
				if (drawPlayer.Male)
				{
					texture = mod.GetTexture("Glowmasks/PrecursorBreastplate_Glow");
					color *= 0.5f;
				}
				else
				{
					texture = mod.GetTexture("Glowmasks/PrecursorBreastplateFemale_Glow");
					color *= 0.5f;
				}
			}
			if (modPlayer.SetOverclockDraw)
			{
				if (drawPlayer.Male)
				{
					texture = mod.GetTexture("Glowmasks/OverclockSuit_Glow");
					color *= 0.75f;
				}
				else
				{
					texture = mod.GetTexture("Glowmasks/OverclockSuitFemale_Glow");
					color *= 0.75f;
				}
			}

			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawPlayer.bodyFrame.Size() / 2, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.bodyArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});

		//Arms
		public static readonly PlayerLayer ArmsGlow = new PlayerLayer("ClickerClass", "ArmsGlow", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;

			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ClickerClass");

			if (modPlayer.SetMiceDraw)
			{
				texture = mod.GetTexture("Glowmasks/MiceSuitArm_Glow");
			}
			if (modPlayer.SetPrecursorDraw)
			{
				texture = mod.GetTexture("Glowmasks/PrecursorBreastplateArm_Glow");
				color *= 0.5f;
			}
			if (modPlayer.SetOverclockDraw)
			{
				texture = mod.GetTexture("Glowmasks/OverclockSuitArm_Glow");
				color *= 0.75f;
			}

			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawPlayer.bodyFrame.Size() / 2, drawPlayer.bodyFrame, color, drawPlayer.bodyRotation, drawInfo.bodyOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.bodyArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});

		//Legs
		public static readonly PlayerLayer LegsGlow = new PlayerLayer("ClickerClass", "LegsGlow", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();
			Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
			Texture2D texture = null;

			if (drawInfo.shadow != 0f || drawInfo.drawPlayer.invis)
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ClickerClass");

			if (modPlayer.SetMiceDraw)
			{
				texture = mod.GetTexture("Glowmasks/MiceBoots_Glow");
			}
			if (modPlayer.SetPrecursorDraw)
			{
				texture = mod.GetTexture("Glowmasks/PrecursorGreaves_Glow");
				color *= 0.5f;
			}
			if (modPlayer.SetOverclockDraw)
			{
				texture = mod.GetTexture("Glowmasks/OverclockBoots_Glow");
				color *= 0.75f;
			}

			if (texture == null)
			{
				return;
			}

			Vector2 drawPos = drawInfo.position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.legFrame.Width / 2, drawPlayer.height - drawPlayer.legFrame.Height + 4f) + drawPlayer.legPosition;
			DrawData drawData = new DrawData(texture, drawPos.Floor() + drawInfo.legOrigin, drawPlayer.legFrame, color, drawPlayer.legRotation, drawInfo.legOrigin, 1f, drawInfo.spriteEffects, 0)
			{
				shader = drawInfo.legArmorShader
			};
			Main.playerDrawData.Add(drawData);
		});

		public static readonly PlayerLayer WeaponGlow = new PlayerLayer("ClickerClass", "WeaponGlow", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			if (drawInfo.shadow != 0f || drawPlayer.dead || drawPlayer.frozen || drawPlayer.itemAnimation <= 0)
			{
				return;
			}
			Mod mod = ModLoader.GetMod("ClickerClass");

			//Fragment Pickaxe
			if (drawPlayer.HeldItem.type == ModContent.ItemType<MicePickaxe>())
			{
				Texture2D weaponGlow = mod.GetTexture("Glowmasks/MicePickaxe_Glow");
				Vector2 position = new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y));
				Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
				DrawData drawData = new DrawData(weaponGlow, position, null, new Color(255, 255, 255, 0) * 0.8f, drawPlayer.itemRotation, origin, drawPlayer.HeldItem.scale, drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(drawData);
			}

			//Fragment Hamaxe
			if (drawPlayer.HeldItem.type == ModContent.ItemType<MiceHamaxe>())
			{
				Texture2D weaponGlow = mod.GetTexture("Glowmasks/MiceHamaxe_Glow");
				Vector2 position = new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y));
				Vector2 origin = new Vector2(drawPlayer.direction == -1 ? weaponGlow.Width : 0, drawPlayer.gravDir == -1 ? 0 : weaponGlow.Height);
				DrawData drawData = new DrawData(weaponGlow, position, null, new Color(255, 255, 255, 0) * 0.8f, drawPlayer.itemRotation, origin, drawPlayer.HeldItem.scale, drawInfo.spriteEffects, 0);
				Main.playerDrawData.Add(drawData);
			}
		});

		public static readonly PlayerLayer MiscEffects = new PlayerLayer("ClickerClass", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			ClickerPlayer modPlayer = drawPlayer.GetModPlayer<ClickerPlayer>();

			if (drawInfo.shadow != 0f || drawPlayer.dead) return;

			if (Main.gameMenu) return;

			if (modPlayer.clickerSelected && modPlayer.clickerDrawRadius)
			{
				if (modPlayer.SetMotherboardDraw)
				{
					Mod mod = ModLoader.GetMod("ClickerClass");
					float glow = modPlayer.clickerInRangeMotherboard ? 0.6f : 0f;

					Color outer = modPlayer.clickerColor * (0.2f + glow);
					int drawX = (int)(drawPlayer.Center.X - Main.screenPosition.X);
					int drawY = (int)(drawPlayer.Center.Y + drawPlayer.gfxOffY - Main.screenPosition.Y);
					Vector2 center = new Vector2(drawX, drawY);
					Vector2 drawPos = center + modPlayer.CalculateMotherboardPosition().Floor();

					Texture2D texture = mod.GetTexture("Glowmasks/MotherboardSetBonus_Glow");
					DrawData drawData = new DrawData(texture, drawPos, null, Color.White, 0f, texture.Size() / 2, 1f, SpriteEffects.None, 0)
					{
						ignorePlayerRotation = true
					};
					Main.playerDrawData.Add(drawData);

					Rectangle frame = new Rectangle(0, 0, 30, 30);
					frame.Y += 30 * modPlayer.setMotherboardFrame;

					texture = mod.GetTexture("Glowmasks/MotherboardSetBonus2_Glow");
					drawData = new DrawData(texture, drawPos, frame, new Color(255, 255, 255, 100) * modPlayer.setMotherboardAlpha, 0f, new Vector2(texture.Width / 2, frame.Height / 2), 1f, SpriteEffects.None, 0)
					{
						ignorePlayerRotation = true
					};
					Main.playerDrawData.Add(drawData);
				}
			}
		});
		*/
	}
}