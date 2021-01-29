using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ChlorophyteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Chlorophyte Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.75f);
			SetColor(Item, new Color(175, 255, 100, 0));
			SetDust(Item, 89);
			SetAmount(Item, 10);
			SetEffect(Item, "Toxic Release");


			Item.damage = 54;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 250000;
			Item.rare = 7;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ChlorophyteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
