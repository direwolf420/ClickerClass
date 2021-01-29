using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class ChocolateMilkCookies : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Chocolate Milk n' Cookies");
			Tooltip.SetDefault("While equipped, cookies will periodically spawn within your clicker radius"
							+ "\nClick the cookie to gain bonus clicker damage, radius, and life regeneration"
							+ "\nGain up to 15% clicker damage based on your amount of clicks within a second"
							+ "\nEvery 15 clicks releases a burst of damaging chocolate");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 85000;
			Item.rare = 5;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.accChocolateChip = true;
			clickerPlayer.accCookie2 = true;
			clickerPlayer.accGlassOfMilk = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ChocolateChip>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MilkCookies>(), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}
	}
}
