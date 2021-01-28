using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class RedHotClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Red Hot Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.6f);
			SetColor(Item, new Color(255, 125, 0, 0));
			SetDust(Item, 174);
			SetAmount(Item, 8);
			SetEffect(Item, "Inferno");


			Item.damage = 17;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 27000;
			Item.rare = 3;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HellstoneBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
