using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LeadClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lead Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.3f);
			SetColor(Item, new Color(75, 75, 125, 0));
			SetDust(Item, 82);
			SetAmount(Item, 10);
			SetEffect(Item, "Double Click");


			Item.damage = 5;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 2700;
			Item.rare = 0;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LeadBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
