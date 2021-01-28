using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CopperClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Copper Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.1f);
			SetColor(Item, new Color(255, 150, 75, 0));
			SetDust(Item, 9);
			SetAmount(Item, 10);
			SetEffect(Item, "Double Click");


			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 450;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CopperBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
