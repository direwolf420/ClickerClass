using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class WoodenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Wooden Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 0.85f);
			SetColor(Item, new Color(125, 100, 75, 0));
			SetDust(Item, 0);

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 24;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
