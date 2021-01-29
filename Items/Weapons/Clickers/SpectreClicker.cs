using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SpectreClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Spectre Clicker");
			Tooltip.SetDefault("Click on an enemy within sight to damage them");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 5f);
			SetColor(Item, new Color(100, 255, 255, 0));
			SetDust(Item, 88);
			SetAmount(Item, 1);
			SetEffect(Item, "Phase Reach");


			Item.damage = 50;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 450000;
			Item.rare = 8;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SpectreBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
