using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CrystalClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Crystal Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.1f);
			SetColor(Item, new Color(200, 50, 255, 0));
			SetDust(Item, 86);
			SetAmount(Item, 8);
			SetEffect(Item, "Dazzle");


			Item.damage = 34;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 90000;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CrystalShard, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
