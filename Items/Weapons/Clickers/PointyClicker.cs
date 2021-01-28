using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class PointyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Pointy Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.35f);
			SetColor(Item, new Color(100, 175, 75, 0));
			SetDust(Item, 39);
			SetAmount(Item, 8);
			SetEffect(Item, "Stinging Thorn");


			Item.damage = 12;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 27000;
			Item.rare = 3;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.JungleSpores, 8);
			recipe.AddIngredient(ItemID.Stinger, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
