using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class MiceMask : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mice Mask");
			Tooltip.SetDefault("Increases click damage by 4%"
							+ "\nIncreases click critical strike chance by 6%");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 70000;
			Item.rare = 10;
			Item.defense = 18;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerDamage += 0.04f;
			clickerPlayer.clickerCrit += 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MiceSuit>() && legs.type == ModContent.ItemType<MiceBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Right clicking a position within your clicker radius will teleport you to it";
			player.GetModPlayer<ClickerPlayer>().setMice = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<MiceFragment>(), 10);
			recipe.AddIngredient(ItemID.LunarBar, 8);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}