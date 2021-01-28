using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class IronClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Iron Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.25f);
			SetColor(Item, new Color(150, 125, 125, 0));
			SetDust(Item, 8);
			SetAmount(Item, 10);
			SetEffect(Item, "Double Click");


			Item.damage = 5;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 1800;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IronBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
