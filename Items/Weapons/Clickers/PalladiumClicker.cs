using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class PalladiumClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Palladium Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(item, 2.85f);
			SetColor(item, new Color(250, 150, 100, 0));
			SetDust(item, 144);
			SetAmount(item, 8);
			SetEffect(item, "Regenerate");


			item.damage = 25;
			item.width = 30;
			item.height = 30;
			item.knockBack = 2f;
			item.value = 92000;
			item.rare = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PalladiumBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
