using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TungstenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Tungsten Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.45f);
			SetColor(Item, new Color(125, 175, 150, 0));
			SetDust(Item, 83);
			SetAmount(Item, 8);
			SetEffect(Item, "Double Click");


			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 6750;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TungstenBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
