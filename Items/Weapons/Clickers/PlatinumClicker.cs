using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class PlatinumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Platinum Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.65f);
			SetColor(Item, new Color(125, 150, 175, 0));
			SetDust(Item, 11);
			SetAmount(Item, 8);
			SetEffect(Item, "Double Click");


			Item.damage = 8;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 13500;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PlatinumBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
