using ClickerClass.Buffs;
using ClickerClass.Items;
using ClickerClass.Items.Accessories;
using ClickerClass.Items.Weapons.Clickers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using static Terraria.GameContent.ItemDropRules.Chains;

namespace ClickerClass.NPCs
{
	public class ClickerGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public bool gouge = false;
		public bool frozen = false;
		public bool honeySlow = false;
		public bool embrittle = false;

		public override void ResetEffects(NPC npc)
		{
			gouge = false;
			frozen = false;
			honeySlow = false;
			embrittle = false;
		}

		public override void SetDefaults(NPC npc)
		{
			switch (npc.type)
			{
				case NPCID.SkeletronHead:
				case NPCID.SkeletronHand:
				case NPCID.SkeletronPrime:
				case NPCID.TheDestroyer:
				case NPCID.Retinazer:
				case NPCID.Spazmatism:
				case NPCID.EyeofCthulhu:
				case NPCID.EaterofWorldsHead:
				case NPCID.EaterofWorldsBody:
				case NPCID.EaterofWorldsTail:
				case NPCID.QueenBee:
				case NPCID.WallofFlesh:
				case NPCID.WallofFleshEye:
				case NPCID.DukeFishron:
				case NPCID.BrainofCthulhu:
				case NPCID.Plantera:
				case NPCID.PrimeCannon:
				case NPCID.PrimeSaw:
				case NPCID.PrimeVice:
				case NPCID.PrimeLaser:
				case NPCID.Golem:
				case NPCID.GolemHead:
				case NPCID.GolemFistLeft:
				case NPCID.GolemFistRight:
				case NPCID.GolemHeadFree:
				case NPCID.CultistBoss:
				case NPCID.KingSlime:
				case NPCID.MoonLordHead:
				case NPCID.MoonLordHand:
				case NPCID.MoonLordCore:
				case NPCID.WyvernHead:
				case NPCID.WyvernLegs:
				case NPCID.WyvernBody:
				case NPCID.WyvernBody2:
				case NPCID.WyvernBody3:
				case NPCID.WyvernTail:
				case NPCID.IceGolem:
				case NPCID.Paladin:
				case NPCID.Everscream:
				case NPCID.IceQueen:
				case NPCID.SantaNK1:
				case NPCID.BigMimicCorruption:
				case NPCID.BigMimicCrimson:
				case NPCID.BigMimicHallow:
				case NPCID.Mothron:
				case NPCID.SandElemental:
				case NPCID.DD2Betsy:
				case NPCID.DD2DarkMageT1:
				case NPCID.DD2DarkMageT3:
				case NPCID.DD2OgreT2:
				case NPCID.DD2OgreT3:
				case NPCID.TargetDummy:
					npc.buffImmune[ModContent.BuffType<Frozen>()] = true;
					npc.buffImmune[ModContent.BuffType<HoneySlow>()] = true;
					break;
				default:
					if (npc.boss)
					{
						goto case NPCID.TargetDummy;
					}
					break;
			}
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (gouge)
			{
				if (npc.lifeRegen > 0) { npc.lifeRegen = 0; }
				npc.lifeRegen -= 60;
				damage = 10;
			}
			if (honeySlow)
			{
				npc.velocity.X *= 0.75f;
				npc.velocity.Y += !npc.noTileCollide ? 0.10f : 0.01f;
			}
			if (frozen)
			{
				npc.position = npc.oldPosition;
				npc.frameCounter = 0;
			}
		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.MoonLordCore)
			{
				int matchID = ItemID.LunarFlareBook;
				int itemToInsert = ItemID.DirtBlock;

				OneFromOptionsNotScaledWithLuckDropRule myRule = null;

				foreach (var drop in npcLoot.Get())
				{
					if (drop is LeadingConditionRule leadingRule &&
						leadingRule.ChainedRules.Count > 0 &&
						leadingRule.ChainedRules[0] is TryIfSucceeded t &&
						t.RuleToChain is OneFromOptionsNotScaledWithLuckDropRule actualRule &&
						actualRule._dropIds.Contains(matchID) &&
						!actualRule._dropIds.Contains(itemToInsert))
					{
						myRule = actualRule;
						break;
					}
				}

				if (myRule != null)
				{
					var drops = myRule._dropIds.ToList();
					drops.Add(itemToInsert);
					myRule._dropIds = drops.ToArray();
				}
			}

			//This method is called once when the game loads (per NPC), so you can't make dynamic checks based on world state like "npc.value > 0f" here
			if (npc.type == NPCID.GoblinSorcerer)
			{
				//20 is the dropsOutOfY argument, meaning its a 1/20 roll aka old Main.rand.NextBool(20)
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowyClicker>(), 20));
			}
			else if (npc.type == NPCID.Frankenstein || npc.type == NPCID.SwampThing)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EclipticClicker>(), 25));
			}
			else if (npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HemoClicker>(), 25));
			}
			else if (npc.type == NPCID.DarkCaster)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Milk>(), 15));
			}
			else if (npc.type == NPCID.Gastropod)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChocolateChip>(), 20));
			}
			else if (npc.type == NPCID.PirateCaptain)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CaptainsClicker>(), 8));
			}
			else if (npc.type == NPCID.Pumpking)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WitchClicker>(), 10));
			}
			else if (npc.type == NPCID.IceQueen)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrozenClicker>(), 10));
			}
			else if (npc.type == NPCID.MartianSaucerCore)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HighTechClicker>(), 4));
			}

			Conditions.NotExpert notExpert = new Conditions.NotExpert();
			if (npc.type == NPCID.MoonLordCore)
			{
				npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<LordsClicker>()));
				npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<TheClicker>(), 5));
			}
			else if (npc.type == NPCID.WallofFlesh)
			{
				npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<ClickerEmblem>(), 4));
			}
			else if (npc.type == NPCID.KingSlime)
			{
				npcLoot.Add(ItemDropRule.ByCondition(notExpert, ModContent.ItemType<StickyKeychain>(), 4));
			}
			else if (npc.type == NPCID.LunarTowerStardust || npc.type == NPCID.LunarTowerSolar || npc.type == NPCID.LunarTowerVortex || npc.type == NPCID.LunarTowerNebula)
			{
				int miceFragment = ModContent.ItemType<MiceFragment>();

				var pNormal = default(DropOneByOne.Parameters);
				pNormal.DropsXOutOfYTimes_TheX = 1;
				pNormal.DropsXOutOfYTimes_TheY = 1;
				pNormal.MinimumStackPerChunkBase = 1;
				pNormal.MaximumStackPerChunkBase = 1;
				pNormal.BonusMinDropsPerChunkPerPlayer = 0;
				pNormal.BonusMaxDropsPerChunkPerPlayer = 0;

				pNormal.MinimumItemDropsCount = 3;
				pNormal.MaximumItemDropsCount = 15;

				var pExpert = pNormal; //Since DropOneByOne.Parameters is a struct, this is a copy/new assignment
				pExpert.MinimumItemDropsCount = 5;
				pExpert.MaximumItemDropsCount = 22;

				var normalModeRule = new DropOneByOne(miceFragment, pNormal);
				var expertModeRule = new DropOneByOne(miceFragment, pExpert);
				npcLoot.Add(new DropBasedOnExpertMode(normalModeRule, expertModeRule));
			}
		}

		/*
		//Old code to compare with
		public override void OnKill(NPC npc)
		{
			if (npc.type == NPCID.GoblinSorcerer && npc.value > 0f)
			{
				if (Main.rand.NextBool(20))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<ShadowyClicker>(), 1, false, -1);
				}
			}
			if ((npc.type == NPCID.Frankenstein || npc.type == NPCID.SwampThing) && npc.value > 0f)
			{
				if (Main.rand.NextBool(25))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<EclipticClicker>(), 1, false, -1);
				}
			}
			if ((npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler) && npc.value > 0f)
			{
				if (Main.rand.NextBool(25))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<HemoClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.DarkCaster)
			{
				if (Main.rand.NextBool(15))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<Milk>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.Gastropod)
			{
				if (Main.rand.NextBool(20))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<ChocolateChip>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.PirateCaptain)
			{
				if (Main.rand.NextBool(8))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<CaptainsClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.Pumpking)
			{
				if (Main.rand.NextBool(10))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<WitchClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.IceQueen)
			{
				if (Main.rand.NextBool(10))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<FrozenClicker>(), 1, false, -1);
				}
			}
			if (npc.type == NPCID.MartianSaucerCore)
			{
				if (Main.rand.NextBool(4))
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<HighTechClicker>(), 1, false, -1);
				}
			}
			if (!Main.expertMode)
			{
				if (npc.type == NPCID.MoonLordCore)
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<LordsClicker>(), 1, false, -1);

					if (Main.rand.NextBool(5))
					{
						Item.NewItem(npc.Hitbox, ModContent.ItemType<TheClicker>(), 1, false, -1);
					}
				}
				if (npc.type == NPCID.WallofFlesh)
				{
					if (Main.rand.NextBool(4))
					{
						Item.NewItem(npc.Hitbox, ModContent.ItemType<ClickerEmblem>(), 1, false, -1);
					}
				}
				if (npc.type == NPCID.KingSlime)
				{
					if (Main.rand.NextBool(4))
					{
						Item.NewItem(npc.Hitbox, ModContent.ItemType<StickyKeychain>(), 1, false, -1);
					}
				}
			}
			if (npc.type == NPCID.LunarTowerStardust || npc.type == NPCID.LunarTowerSolar || npc.type == NPCID.LunarTowerVortex || npc.type == NPCID.LunarTowerNebula)
			{
				if (Main.expertMode)
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<MiceFragment>(), Main.rand.Next(5, 23));
				}
				else
				{
					Item.NewItem(npc.Hitbox, ModContent.ItemType<MiceFragment>(), Main.rand.Next(3, 16));
				}
			}
		}
		*/

		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			Player closestPlayer = Main.LocalPlayer;

			switch (type)
			{
				case NPCID.Merchant:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Cookie>());
					break;
				case NPCID.TravellingMerchant:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<Soda>());
					break;
				case NPCID.Mechanic:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<HandCream>());
					break;
				case NPCID.GoblinTinkerer:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<MousePad>());
					break;
				case NPCID.SkeletonMerchant:
					shop.item[nextSlot++].SetDefaults(ModContent.ItemType<CandleClicker>());
					break;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (gouge)
			{
				if (!Main.rand.NextBool(4))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 5, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 2.2f);
					Dust dust2 = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 54, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.6f);
					dust.noGravity = true;
					dust.velocity *= 1.2f;
					dust.velocity.Y -= 0.7f;
					dust2.noGravity = true;
					dust2.velocity *= 1.2f;
					dust2.velocity.Y -= 0.7f;
					if (Main.rand.NextBool(4))
					{
						dust.noGravity = false;
						dust.scale *= 0.3f;
						dust2.noGravity = false;
						dust2.scale *= 0.3f;
					}
				}
				drawColor.G = (byte)(drawColor.G * 0.35f);
				drawColor.B = (byte)(drawColor.B * 0.15f);
			}
			if (honeySlow)
			{
				if (Main.rand.Next(4) < 3)
				{
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 153, 0f, Main.rand.NextFloat(0.25f, 0.75f), 50, default(Color), 1.35f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].fadeIn = 1.1f;
					if (Main.rand.Next(4) == 0)
					{
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
			}
			if (frozen)
			{
				if (!Main.rand.NextBool(5))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 111, 0f, 0f, 0, default(Color), 0.75f);
					dust.noGravity = true;
					dust.velocity *= 0f;
					dust.fadeIn = 1.75f;
				}
				Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.6f);
				drawColor.R = (byte)(drawColor.R * 0.35f);
				drawColor.G = (byte)(drawColor.G * 0.65f);
			}
			if (embrittle)
			{
				if (!Main.rand.NextBool(5))
				{
					Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 57, 0f, 0f, 255, default(Color), 0.65f);
					dust.shader = GameShaders.Armor.GetSecondaryShader(20, Main.LocalPlayer);
					dust.noGravity = true;
					dust.velocity *= 0f;
					dust.fadeIn = 1.75f;
				}
				drawColor.R = (byte)(drawColor.R * 1f);
				drawColor.G = (byte)(drawColor.G * 1f);
				drawColor.B = (byte)(drawColor.B * 1f);
			}
		}
	}
}
