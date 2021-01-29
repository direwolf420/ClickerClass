using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SinisterClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Sinister Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.2f);
			SetColor(Item, new Color(100, 25, 25, 0));
			SetDust(Item, 5);
			SetAmount(Item, 10);
			SetEffect(Item, "Siphon");


			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 18000;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
