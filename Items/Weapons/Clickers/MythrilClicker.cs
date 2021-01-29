using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class MythrilClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mythril Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.95f);
			SetColor(Item, new Color(125, 225, 125, 0));
			SetDust(Item, 49);
			SetAmount(Item, 10);
			SetEffect(Item, "Embrittle");


			Item.damage = 25;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 103500;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.MythrilBar, 8);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
