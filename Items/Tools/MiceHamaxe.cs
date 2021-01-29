using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Tools
{
	public class MiceHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mice Hamaxe");
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 7;
			Item.useAnimation = 28;
			Item.tileBoost = 4;
			Item.axe = 30;
			Item.hammer = 100;
			Item.useStyle = 1;
			Item.knockBack = 7f;
			Item.rare = 10;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<MiceFragment>(), 14);
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}