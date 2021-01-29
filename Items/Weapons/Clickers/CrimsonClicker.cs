using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CrimsonClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Crimson Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.9f);
			SetColor(Item, new Color(255, 225, 175, 0));
			SetDust(Item, 87);
			SetAmount(Item, 10);
			SetEffect(Item, "Infest");


			Item.damage = 30;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 105000;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Ichor, 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
