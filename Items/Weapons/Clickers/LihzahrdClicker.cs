using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LihzahrdClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lihzahrd Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 4f);
			SetColor(Item, new Color(200, 75, 0, 0));
			SetDust(Item, 174);
			SetAmount(Item, 10);
			SetEffect(Item, "Solar Flare");


			Item.damage = 66;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 300000;
			Item.rare = 7;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
