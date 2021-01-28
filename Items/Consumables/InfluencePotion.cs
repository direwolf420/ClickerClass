using ClickerClass.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Consumables
{
	public class InfluencePotion : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Influence Potion");
			Tooltip.SetDefault("Increases your base click radius by 20%");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.useStyle = 2;
			Item.useTurn = true;
			Item.value = Item.sellPrice(0, 0, 2, 0);
			Item.consumable = true;
			Item.maxStack = 30;
			Item.rare = 1;
			Item.UseSound = SoundID.Item3;
			Item.buffType = ModContent.BuffType<InfluenceBuff>();
			Item.buffTime = 18000;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater, 1);
			recipe.AddIngredient(ItemID.Daybloom, 1);
			recipe.AddIngredient(ItemID.PinkGel, 1);
			recipe.AddTile(TileID.Bottles);
			recipe.Register();
		}
	}
}