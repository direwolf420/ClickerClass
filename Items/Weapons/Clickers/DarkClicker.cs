using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class DarkClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Dark Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.1f);
			SetColor(Item, new Color(100, 50, 200, 0));
			SetDust(Item, 14);
			SetAmount(Item, 8);
			SetEffect(Item, "Dark Burst");


			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 18000;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DemoniteBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
