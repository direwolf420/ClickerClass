using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class MiceSuit : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mice Suit");
			Tooltip.SetDefault("Increases click damage by 10%"
							+ "\nIncreases your base click radius by 25%");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 140000;
			Item.rare = 10;
			Item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerDamage += 0.10f;
			clickerPlayer.clickerRadius += 0.5f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<MiceFragment>(), 20);
			recipe.AddIngredient(ItemID.LunarBar, 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}