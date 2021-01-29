using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CobaltClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Cobalt Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(50, 125, 200, 0));
			SetDust(Item, 48);
			SetAmount(Item, 5);
			SetEffect(Item, "Haste");


			Item.damage = 24;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 45000;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CobaltBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
