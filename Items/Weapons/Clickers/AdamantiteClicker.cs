using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AdamantiteClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Adamantite Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();

			SetRadius(Item, 3.15f);
			SetColor(Item, new Color(255, 25, 25, 0));
			SetDust(Item, 50);
			SetAmount(Item, 10);
			SetEffect(Item, "True Strike");

			Item.damage = 32;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 138000;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.AdamantiteBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
