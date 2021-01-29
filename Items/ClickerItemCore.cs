﻿using ClickerClass.Buffs;
using ClickerClass.Dusts;
using ClickerClass.Prefixes;
using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ClickerClass.Items
{
	/// <summary>
	/// The class responsible for any clicker item related logic
	/// </summary>
	public class ClickerItemCore : GlobalItem
	{
		public override bool InstancePerEntity => true;

		/// <summary>
		/// A clickers color used for various things
		/// </summary>
		public Color clickerColorItem = Color.White;

		/// <summary>
		/// The amount of clicks required for an effect to trigger
		/// </summary>
		public int itemClickerAmount = 0;

		public const string NULL = "NULL";

		/// <summary>
		/// The clickers effect name
		/// </summary>
		public string itemClickerEffect = NULL;

		/// <summary>
		/// The clickers dust that is spawned on use
		/// </summary>
		public int clickerDustColor = 0;
		
		/// <summary>
		/// Displays total clicks in the tooltip
		/// </summary>
		public bool isClickerDisplayTotal = false;

		/// <summary>
		/// Additional range for this clicker (1f = 100 pixel, 1f by default from the player)
		/// </summary>
		public float radiusBoost = 0f;

		internal float radiusBoostPrefix = 0f;
		internal int clickBoostPrefix = 0;

		public override GlobalItem Clone(Item item, Item itemClone)
		{
			ClickerItemCore myClone = (ClickerItemCore)base.Clone(item, itemClone);
			myClone.clickerColorItem = clickerColorItem;
			myClone.itemClickerAmount = itemClickerAmount;
			myClone.itemClickerEffect = itemClickerEffect;
			myClone.clickerDustColor = clickerDustColor;
			myClone.clickBoostPrefix = clickBoostPrefix;
			myClone.isClickerDisplayTotal = isClickerDisplayTotal;
			myClone.radiusBoost = radiusBoost;
			myClone.radiusBoostPrefix = radiusBoostPrefix;
			return myClone;
		}

		public override float MeleeSpeedMultiplier(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				if (!player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					if (player.GetModPlayer<ClickerPlayer>().clickerAutoClick || item.autoReuse)
					{
						return 10f;
					}
					else
					{
						return 1f;
					}
				}
				else
				{
					return 9.5f;
				}
			}

			return base.MeleeSpeedMultiplier(item, player);
		}

		public override float UseTimeMultiplier(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				if (!player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					if (player.GetModPlayer<ClickerPlayer>().clickerAutoClick || item.autoReuse)
					{
						return 0.1f;
					}
					else
					{
						return 1f;
					}
				}
				else
				{
					return 0.15f;
				}
			}

			return base.UseTimeMultiplier(item, player);
		}

		public override bool CanUseItem(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				if (clickerPlayer.clickerAutoClick || player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					item.autoReuse = true;
				}
				else
				{
					item.autoReuse = false;
				}

				if (!itemClickerEffect.Contains("Phase Reach"))
				{
					//collision
					Vector2 motherboardPosition = clickerPlayer.setMotherboardPosition;
					bool inRange = Vector2.Distance(Main.MouseWorld, player.Center) < clickerPlayer.ClickerRadiusReal && Collision.CanHit(new Vector2(player.Center.X, player.Center.Y - 12), 1, 1, Main.MouseWorld, 1, 1);
					bool inRangeMotherboard = Vector2.Distance(Main.MouseWorld, motherboardPosition) < clickerPlayer.ClickerRadiusMotherboard && Collision.CanHit(motherboardPosition, 1, 1, Main.MouseWorld, 1, 1);
					//bool allowMotherboard = player.GetModPlayer<ClickerPlayer>().clickerMotherboardSet && player.altFunctionUse == 2;

					if (inRange || (inRangeMotherboard && player.altFunctionUse != 2))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			return base.CanUseItem(item, player);
		}

		public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage, ref float flat)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				flat += clickerPlayer.clickerDamageFlat;
				damage.multiplicative *= clickerPlayer.clickerDamage;
			}
		}

		public override void ModifyWeaponCrit(Item item, Player player, ref int crit)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				crit += player.GetModPlayer<ClickerPlayer>().clickerCrit;
			}
		}

		public override bool AltFunctionUse(Item item, Player player)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				if (clickerPlayer.setMice || clickerPlayer.setMotherboard)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return base.AltFunctionUse(item, player);
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (ClickerSystem.IsClickerItem(item))
			{
				Player player = Main.LocalPlayer;
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				int index;
				
				float alpha = Main.mouseTextColor / 255f;

				if (ClickerConfigClient.Instance.ShowClassTags)
				{
					index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("ItemName"));
					if (index != -1)
					{
						tooltips.Insert(index + 1, new TooltipLine(Mod, "ClickerTag", "-Clicker Class-")
						{
							overrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
						});
					}
				}

				if (isClickerDisplayTotal)
				{
					index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("Tooltip0"));

					if (index != -1)
					{
						string color = (new Color(252, 210, 44) * alpha).Hex3();
						tooltips.Add(new TooltipLine(Mod, "transformationText", "Total clicks: " + $"[c/" + color + ":" + clickerPlayer.clickerTotal + "]"));
					}
				}

				if (ClickerSystem.IsClickerWeapon(item))
				{
					if (!itemClickerEffect.Contains("The Click"))
					{
						TooltipLine tooltip = tooltips.Find(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("Damage"));
						if (tooltip != null)
						{
							tooltip.text = tooltip.text.Split(' ')[0] + " click damage";
						}
					}
					else
					{
						TooltipLine tooltip = tooltips.Find(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("Damage"));
						if (tooltip != null)
						{
							tooltip.text = tooltip.text.Split(' ')[0] + " + 1% enemy life click damage";
						}
					}

					if (itemClickerAmount > 0 && itemClickerEffect != NULL)
					{
						index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("Knockback"));

						if (index != -1)
						{
							string color = (clickerColorItem * alpha).Hex3();
							int clickAmountTotal = clickerPlayer.GetClickAmountTotal(this);
							if (clickAmountTotal > 1)
							{
								tooltips.Insert(index + 1, new TooltipLine(Mod, "itemClickerAmount", clickAmountTotal + " clicks - " + $"[c/" + color + ":" + itemClickerEffect + "]"));
							}
							else
							{
								tooltips.Insert(index + 1, new TooltipLine(Mod, "itemClickerAmount", "1 click - " + $"[c/" + color + ":" + itemClickerEffect + "]"));
							}
						}
					}
				}

				if (item.prefix < PrefixID.Count || !ClickerPrefix.ClickerPrefixes.Contains(item.prefix))
				{
					return;
				}

				if (radiusBoostPrefix != 0)
				{
					int ttindex = tooltips.FindLastIndex(t => (t.mod == "Terraria" || t.mod == Mod.Name) && (t.isModifier || t.Name.StartsWith("Tooltip") || t.Name.Equals("Material")));
					if (ttindex != -1)
					{
						TooltipLine tt = new TooltipLine(Mod, "PrefixClickerRadius", (radiusBoostPrefix > 0 ? "+" : "") + (int)((radiusBoostPrefix / 2) * 100) + "% base clicker radius")
						{
							isModifier = true,
							isModifierBad = radiusBoostPrefix < 0
						};
						tooltips.Insert(ttindex + 1, tt);
					}
				}

				if (clickBoostPrefix != 0)
				{
					int ttindex = tooltips.FindLastIndex(t => (t.mod == "Terraria" || t.mod == Mod.Name) && (t.isModifier || t.Name.StartsWith("Tooltip") || t.Name.Equals("Material")));
					if (ttindex != -1)
					{
						TooltipLine tt = new TooltipLine(Mod, "PrefixClickBoost", (clickBoostPrefix < 0 ? "" : "+") + clickBoostPrefix + " clicks required")
						{
							isModifier = true,
							isModifierBad = clickBoostPrefix > 0
						};
						tooltips.Insert(ttindex + 1, tt);
					}
				}
			}
		}

		public override bool PreReforge(Item item)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				radiusBoostPrefix = 0f;
				clickBoostPrefix = 0;
			}
			return base.PreReforge(item);
		}

		public override int ChoosePrefix(Item item, UnifiedRandom rand)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				if (item.maxStack == 1 && item.useStyle > 0)
				{
					return rand.Next(ClickerPrefix.ClickerPrefixes);
				}
			}
			return base.ChoosePrefix(item, rand);
		}

		public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (ClickerSystem.IsClickerWeapon(item))
			{
				var clickerPlayer = player.GetModPlayer<ClickerPlayer>();
				if (player.altFunctionUse == 2)
				{
					//Right click 
					if (clickerPlayer.setAbilityDelayTimer <= 0)
					{
						//Mice armor 
						if (clickerPlayer.setMice)
						{
							bool canTeleport = false;
							if (!itemClickerEffect.Contains("Phase Reach"))
							{
								//collision
								if (Vector2.Distance(Main.MouseWorld, player.Center) < clickerPlayer.ClickerRadiusReal && Collision.CanHitLine(new Vector2(player.Center.X, player.Center.Y - 12), 1, 1, Main.MouseWorld, 1, 1))
								{
									canTeleport = true;
								}
							}
							else
							{
								canTeleport = true;
							}

							if (canTeleport)
							{
								SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 115);
								Main.SetCameraLerp(0.1f, 0);
								player.Center = Main.MouseWorld;
								NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);
								player.fallStart = (int)(player.position.Y / 16f);
								clickerPlayer.setAbilityDelayTimer = 60;

								float num102 = 50f;
								int num103 = 0;
								while ((float)num103 < num102)
								{
									Vector2 vector12 = Vector2.UnitX * 0f;
									vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(2f, 2f);
									vector12 = vector12.RotatedBy((double)Vector2.Zero.ToRotation(), default(Vector2));
									int num104 = Dust.NewDust(Main.MouseWorld, 0, 0, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 2f);
									Main.dust[num104].noGravity = true;
									Main.dust[num104].position = Main.MouseWorld + vector12;
									Main.dust[num104].velocity = Vector2.Zero * 0f + vector12.SafeNormalize(Vector2.UnitY) * 4f;
									int num = num103;
									num103 = num + 1;
								}
							}
						}
						else if (clickerPlayer.setMotherboard)
						{
							SoundEngine.PlaySound(SoundID.Camera, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 0);
							clickerPlayer.SetMotherboardRelativePosition(Main.MouseWorld);
							clickerPlayer.setAbilityDelayTimer = 60;
						}
					}
					return false;
				}

				//Base 
				SoundEngine.PlaySound(SoundID.MenuTick, player.position);
				if (!player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					clickerPlayer.clickerPerSecond++;
					clickerPlayer.clickAmount++;
					clickerPlayer.clickerTotal++;
				}

				//TODO "PreShoot" hook wrapping around the next NewProjectile

				//Spawn normal click damage
				Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, type, damage, knockBack, player.whoAmI);

				//Precursor armor set bonus
				if (clickerPlayer.setPrecursor)
				{
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<PrecursorPro>(), (int)(damage * 0.25f), knockBack, player.whoAmI);
				}

				//Find click amount
				int clickAmountTotal = clickerPlayer.GetClickAmountTotal(this);

				//Overclock armor set bonus
				if (clickerPlayer.clickAmount % 100 == 0 && clickerPlayer.setOverclock)
				{
					SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 94);
					player.AddBuff(ModContent.BuffType<OverclockBuff>(), 180, false);
					for (int i = 0; i < 25; i++)
					{
						int num6 = Dust.NewDust(player.position, 20, 20, 90, 0f, 0f, 150, default(Color), 1.35f);
						Main.dust[num6].noGravity = true;
						Main.dust[num6].velocity *= 0.75f;
						int num7 = Main.rand.Next(-50, 51);
						int num8 = Main.rand.Next(-50, 51);
						Dust dust = Main.dust[num6];
						dust.position.X = dust.position.X + (float)num7;
						Dust dust2 = Main.dust[num6];
						dust2.position.Y = dust2.position.Y + (float)num8;
						Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
						Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
					}
				}

				//Special Effects
				if (clickerPlayer.clickAmount % 10 == 0 && clickerPlayer.accStickyKeychain)
				{
					SoundEngine.PlaySound(3, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 13);
					Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<StickyKeychainPro>(), (int)(damage * 0.5), 3f, player.whoAmI, Main.rand.Next(3));
					for (int k = 0; k < 20; k++)
					{
						Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 88, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 175, default, 1.75f);
						dust.noGravity = true;
						dust.noLight = true;
					}
				}
				if (clickerPlayer.clickAmount % 15 == 0 && clickerPlayer.accChocolateChip)
				{
					SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 112);
					for (int k = 0; k < 6; k++)
					{
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f), ModContent.ProjectileType<ChocolateChipPro>(), (int)(damage * 0.2), 0f, player.whoAmI, Main.rand.Next(3), 0f);
					}
					for (int k = 0; k < 20; k++)
					{
						Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 22, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 1.5f);
						dust.noGravity = true;
						dust.noLight = true;
					}
				}

				//Clicker Effects
				if ((clickerPlayer.clickAmount % clickAmountTotal == 0 || player.HasBuff(ModContent.BuffType<OverclockBuff>())) && !player.HasBuff(ModContent.BuffType<AutoClick>()))
				{
					// Pumpkin Moon Clicker Effect
					int wildMagic = 0;
					if (itemClickerEffect.Contains("Wild Magic"))
					{
						wildMagic = 1 + Main.rand.Next(28);
					}

					// Metal Double Click Effect
					if (itemClickerEffect.Contains("Double Click") || wildMagic == 1)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 37);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, type, damage, knockBack, player.whoAmI);
					}

					// Candle Clicker Effect
					if (itemClickerEffect.Contains("Illuminate") || wildMagic == 26)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 74);
						for (int k = 0; k < 15; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 55, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 1.35f);
							dust.noGravity = true;
						}
						for (int k = 0; k < 8; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<CandleClickerPro>(), 0, 0f, player.whoAmI);
						}
					}

					// Hemo Clicker Effect
					if (itemClickerEffect.Contains("Linger") || wildMagic == 2)
					{
						SoundEngine.PlaySound(3, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 13);
						for (int k = 0; k < 5; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-3f, -1f), ModContent.ProjectileType<HemoClickerPro>(), (int)(damage * 0.50f), 0f, player.whoAmI);
						}
					}

					// Bone Clicker Effect
					if (itemClickerEffect.Contains("Lacerate") || wildMagic == 3)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 71);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<BoneClickerPro>(), damage, knockBack, player.whoAmI);
						for (int k = 0; k < 10; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 5, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 125, default, 1.25f);
							dust.noGravity = true;
						}
					}

					// Corruption Clicker Effect
					if (itemClickerEffect.Contains("Dark Burst") || wildMagic == 4)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 70);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<DarkClickerPro>(), damage, knockBack, player.whoAmI);
						for (int k = 0; k < 25; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 14, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 75, default, 1.5f);
							dust.noGravity = true;
						}
					}

					// Crimson Clicker Effect
					if (itemClickerEffect.Contains("Siphon") || wildMagic == 5)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 112);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<SinisterClickerPro>(), (int)(damage * 0.50f), knockBack, player.whoAmI);
						for (int i = 0; i < 15; i++)
						{
							int num6 = Dust.NewDust(Main.MouseWorld, 20, 20, 5, 0f, 0f, 75, default(Color), 1.5f);
							Main.dust[num6].noGravity = true;
							Main.dust[num6].velocity *= 0.75f;
							int num7 = Main.rand.Next(-50, 51);
							int num8 = Main.rand.Next(-50, 51);
							Dust dust = Main.dust[num6];
							dust.position.X = dust.position.X + (float)num7;
							Dust dust2 = Main.dust[num6];
							dust2.position.Y = dust2.position.Y + (float)num8;
							Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
							Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
						}
					}

					// Meteor Clicker Effect
					if (itemClickerEffect.Contains("Star Storm") || wildMagic == 6)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 42);

						for (int k = 0; k < 3; k++)
						{
							Vector2 startSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-100, 101), Main.MouseWorld.Y - 500 + Main.rand.Next(-25, 26));
							Vector2 endSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-25, 26), Main.MouseWorld.Y + Main.rand.Next(-25, 26));
							Vector2 vector = endSpot - startSpot;
							float speed = 5f;
							float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
							if (mag > speed)
							{
								mag = speed / mag;
							}
							vector *= mag;
							Projectile.NewProjectile(startSpot.X, startSpot.Y, vector.X, vector.Y, ModContent.ProjectileType<SpaceClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, endSpot.X, endSpot.Y);
						}
					}

					// Shadowy Clicker Effect
					if (itemClickerEffect.Contains("Curse") || wildMagic == 27)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 104);
						for (int k = 0; k < 15; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 27, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 255, default, 1f);
							dust.noGravity = true;
						}

						int index = -1;
						for (int i = 0; i < 200; i++)
						{
							NPC npc = Main.npc[i];
							if (npc.active && Vector2.Distance(Main.MouseWorld, npc.Center) < 400f && Collision.CanHit(Main.MouseWorld, 1, 1, npc.Center, 1, 1))
							{
								index = i;
							}
						}
						if (index != -1)
						{
							Vector2 vector = Main.npc[index].Center - Main.MouseWorld;
							float speed = 6f;
							float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
							if (mag > speed)
							{
								mag = speed / mag;
							}
							vector *= mag;
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, vector.X, vector.Y, ModContent.ProjectileType<ShadowyClickerPro>(), damage, knockBack, player.whoAmI);
						}
					}

					// Dungeon Clicker Effect
					if (itemClickerEffect.Contains("Splash") || wildMagic == 7)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 86);
						for (int k = 0; k < 6; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-6f, -2f), ModContent.ProjectileType<SlickClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI);
						}
					}

					// Jungle Clicker Effect
					if (itemClickerEffect.Contains("Stinging Thorn") || wildMagic == 8)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 17);

						int index = -1;
						for (int i = 0; i < 200; i++)
						{
							NPC npc = Main.npc[i];
							if (npc.active && Vector2.Distance(Main.MouseWorld, npc.Center) < 400f && Collision.CanHit(Main.MouseWorld, 1, 1, npc.Center, 1, 1))
							{
								index = i;
							}
						}
						if (index != -1)
						{
							Vector2 vector = Main.npc[index].Center - Main.MouseWorld;
							float speed = 3f;
							float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
							if (mag > speed)
							{
								mag = speed / mag;
							}
							vector *= mag;
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, vector.X, vector.Y, ModContent.ProjectileType<PointyClickerPro>(), damage, knockBack, player.whoAmI);
						}
					}

					// Molten Clicker Effect
					if (itemClickerEffect.Contains("Inferno") || wildMagic == 9)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 74);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<RedHotClickerPro>(), 0, knockBack, player.whoAmI);
					}

					// Night Clicker Effect
					if (itemClickerEffect.Contains("Shadow Lash") || wildMagic == 10)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 103);
						for (int k = 0; k < 5; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), ModContent.ProjectileType<UmbralClickerPro>(), (int)(damage * 0.5f), knockBack, player.whoAmI);
						}
					}

					// Cobalt Clicker Effect
					if (itemClickerEffect.Contains("Haste") || wildMagic == 11)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
						player.AddBuff(ModContent.BuffType<Haste>(), 300, false);
						for (int i = 0; i < 15; i++)
						{
							int num6 = Dust.NewDust(player.position, 20, 20, 56, 0f, 0f, 150, default(Color), 1.25f);
							Main.dust[num6].noGravity = true;
							Main.dust[num6].velocity *= 0.75f;
							int num7 = Main.rand.Next(-50, 51);
							int num8 = Main.rand.Next(-50, 51);
							Dust dust = Main.dust[num6];
							dust.position.X = dust.position.X + (float)num7;
							Dust dust2 = Main.dust[num6];
							dust2.position.Y = dust2.position.Y + (float)num8;
							Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
							Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
						}
					}

					// Palladium Clicker Effect
					if (itemClickerEffect.Contains("Regenerate") || wildMagic == 12)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
						player.AddBuff(BuffID.RapidHealing, 120, false);
						for (int i = 0; i < 15; i++)
						{
							int num6 = Dust.NewDust(player.position, 20, 20, ModContent.DustType<LoveDust>(), 0f, 0f, 0, Color.White, 1f);
							Main.dust[num6].noGravity = true;
							Main.dust[num6].velocity *= 0.75f;
							int num7 = Main.rand.Next(-50, 51);
							int num8 = Main.rand.Next(-50, 51);
							Dust dust = Main.dust[num6];
							dust.position.X = dust.position.X + (float)num7;
							Dust dust2 = Main.dust[num6];
							dust2.position.Y = dust2.position.Y + (float)num8;
							Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
							Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
						}
					}

					// Mythril Clicker Effect
					if (itemClickerEffect.Contains("Embrittle") || wildMagic == 13)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 101);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<MythrilClickerPro>(), 0, knockBack, player.whoAmI);
					}

					// Orich Clicker Effect
					if (itemClickerEffect.Contains("Petal Storm") || wildMagic == 14)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);

						for (int k = 0; k < 5; k++)
						{
							float xChoice = Main.rand.Next(-100, 101);
							float yChoice = Main.rand.Next(-100, 101);
							xChoice += xChoice > 0 ? 300 : -300;
							yChoice += yChoice > 0 ? 300 : -300;
							Vector2 startSpot = new Vector2(Main.MouseWorld.X + xChoice, Main.MouseWorld.Y + yChoice);
							Vector2 endSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-10, 11), Main.MouseWorld.Y + Main.rand.Next(-10, 11));
							Vector2 vector = endSpot - startSpot;
							float speed = 4f;
							float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
							if (mag > speed)
							{
								mag = speed / mag;
							}
							vector *= mag;
							Projectile.NewProjectile(startSpot.X, startSpot.Y, vector.X, vector.Y, ModContent.ProjectileType<OrichaclumClickerPro>(), (int)(damage * 0.5f), 0f, player.whoAmI, Main.rand.Next(3), 0f);
						}
					}

					// Adamantite Clicker Effect
					if (itemClickerEffect.Contains("True Strike") || wildMagic == 15)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 71);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<AdamantiteClickerPro>(), damage, knockBack, player.whoAmI);
					}

					// Titanium Clicker Effect
					if (itemClickerEffect.Contains("Razor's Edge") || wildMagic == 16)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 22);
						for (int k = 0; k < 5; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<TitaniumClickerPro>(), (int)(damage * 0.75f), 0f, player.whoAmI, k, 0f);
						}
						for (int k = 0; k < 15; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 91, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.25f);
							dust.noGravity = true;
						}
					}

					// Crystal Clicker Effect
					if (itemClickerEffect.Contains("Dazzle") || wildMagic == 17)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 28);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<CrystalClickerPro>(), 0, knockBack, player.whoAmI);
					}

					// Cursed Clicker Effect
					if (itemClickerEffect.Contains("Cursed Eruption") || wildMagic == 18)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 74);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<CorruptClickerPro>(), damage, knockBack, player.whoAmI);
						for (int k = 0; k < 30; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 163, Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f), 0, default, 1.65f);
							dust.noGravity = true;
						}
					}

					// Ichor Clicker Effect
					if (itemClickerEffect.Contains("Infest") || wildMagic == 19)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
						for (int k = 0; k < 8; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), ModContent.ProjectileType<CrimsonClickerPro>(), (int)(damage * 0.25f), knockBack, player.whoAmI);
						}
					}

					// Pirate Clicker Effect
					if (itemClickerEffect.Contains("Bombard") || wildMagic == 20)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 14);

						for (int k = 0; k < 4; k++)
						{
							Vector2 startSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-150, 151), Main.MouseWorld.Y - 500 + Main.rand.Next(-25, 26));
							Vector2 endSpot = new Vector2(Main.MouseWorld.X + Main.rand.Next(-25, 26), Main.MouseWorld.Y + Main.rand.Next(-25, 26));
							Vector2 vector = endSpot - startSpot;
							float speed = 8f + Main.rand.NextFloat(-1f, 1f);
							float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
							if (mag > speed)
							{
								mag = speed / mag;
							}
							vector *= mag;
							Projectile.NewProjectile(startSpot.X, startSpot.Y, vector.X, vector.Y, ModContent.ProjectileType<CaptainsClickerPro>(), damage, knockBack, player.whoAmI, endSpot.X, endSpot.Y);
						}
					}

					// Eclipse Clicker Effect
					if (itemClickerEffect.Contains("Totality") || wildMagic == 28)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 43);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<TotalityClickerPro>(), damage, knockBack, player.whoAmI);
					}

					// Hallowed Clicker Effect
					if (itemClickerEffect.Contains("Holy Nova") || wildMagic == 21)
					{
						SoundEngine.PlaySound(3, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 5);
						for (int u = 0; u < Main.maxNPCs; u++)
						{
							NPC target = Main.npc[u];
							if (Collision.CanHit(target.Center, 1, 1, Main.MouseWorld, 1, 1) && target.DistanceSQ(Main.MouseWorld) < 350 * 350)
							{
								Vector2 vector = target.Center - Main.MouseWorld;
								float speed = 8f;
								float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
								if (mag > speed)
								{
									mag = speed / mag;
								}
								vector *= mag;
								Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<ArthursClickerPro>(), (int)(damage * 0.75f), knockBack, player.whoAmI, 1f, 0f);

								for (int k = 0; k < 30; k++)
								{
									Dust dust = Dust.NewDustDirect(target.Center, 8, 8, 87, vector.X + Main.rand.NextFloat(-1f, 1f), vector.Y + Main.rand.NextFloat(-1f, 1f), 0, default, 1.25f);
									dust.noGravity = true;
								}
							}
						}

						float num102 = 100f;
						int num103 = 0;
						while ((float)num103 < num102)
						{
							Vector2 vector12 = Vector2.UnitX * 0f;
							vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(2f, 2f);
							vector12 = vector12.RotatedBy((double)Vector2.Zero.ToRotation(), default(Vector2));
							int num104 = Dust.NewDust(Main.MouseWorld, 0, 0, 87, 0f, 0f, 0, default(Color), 2f);
							Main.dust[num104].noGravity = true;
							Main.dust[num104].position = Main.MouseWorld + vector12;
							Main.dust[num104].velocity = Vector2.Zero * 0f + vector12.SafeNormalize(Vector2.UnitY) * 15f;
							int num = num103;
							num103 = num + 1;
						}
					}

					// Chloro Clicker Effect
					if (itemClickerEffect.Contains("Toxic Release") || wildMagic == 22)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 104);
						for (int k = 0; k < 10; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<ChlorophyteClickerPro>(), (int)(damage * 0.5f), 0f, player.whoAmI);
						}
					}

					// Shroom Clicker Effect
					if (itemClickerEffect.Contains("Auto Click") || wildMagic == 23)
					{
						clickerPlayer.clickAmount++;
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 24);
						player.AddBuff(ModContent.BuffType<AutoClick>(), 300, false);
						for (int i = 0; i < 15; i++)
						{
							int num6 = Dust.NewDust(player.position, 20, 20, 15, 0f, 0f, 255, default(Color), 1.5f);
							Main.dust[num6].noGravity = true;
							Main.dust[num6].velocity *= 0.75f;
							int num7 = Main.rand.Next(-50, 51);
							int num8 = Main.rand.Next(-50, 51);
							Dust dust = Main.dust[num6];
							dust.position.X = dust.position.X + (float)num7;
							Dust dust2 = Main.dust[num6];
							dust2.position.Y = dust2.position.Y + (float)num8;
							Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
							Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
						}
					}

					// Temple Clicker Effect
					if (itemClickerEffect.Contains("Solar Flare") || wildMagic == 24)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 68);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<LihzarhdClickerPro>(), (int)(damage * 0.5f), 0f, player.whoAmI);
					}

					// Martian Clicker Effect
					if (itemClickerEffect.Contains("Discharge") || wildMagic == 25)
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 94);
						for (int k = 0; k < 4; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), ModContent.ProjectileType<HighTechClickerPro>(), damage, 0f, player.whoAmI);
						}
						for (int k = 0; k < 20; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, 229, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
							dust.noGravity = true;
						}
					}

					// Mice Clicker Effect
					if (itemClickerEffect.Contains("Collision"))
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 105);
						for (int k = 0; k < 8; k++)
						{
							Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), ModContent.ProjectileType<MiceClickerPro>(), (int)(damage * 0.5f), knockBack, player.whoAmI);
						}
						for (int k = 0; k < 10; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
							dust.noGravity = true;
						}
					}

					// Astral Clicker Effect
					if (itemClickerEffect.Contains("Spiral"))
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 117);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<AstralClickerPro>(), (int)(damage * 3f), 0f, player.whoAmI);
						for (int k = 0; k < 20; k++)
						{
							Dust dust = Dust.NewDustDirect(Main.MouseWorld, 8, 8, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), 0, default, 1.25f);
							dust.noGravity = true;
						}
					}

					// Moon Lord Clicker Effect
					if (itemClickerEffect.Contains("Conqueror"))
					{
						SoundEngine.PlaySound(2, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 88);
						Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ModContent.ProjectileType<LordsClickerPro>(), (int)(damage * 2f), 0f, player.whoAmI);
					}

					wildMagic = 0;
				}
				return false;
			}
			return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}
