using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CorruptClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Corrupt Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.25f);
			SetColor(Item, new Color(125, 255, 0, 0));
			SetDust(Item, 163);
			SetAmount(Item, 8);
			SetEffect(Item, "Cursed Eruption");


			Item.damage = 31;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 105000;
			Item.rare = 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CursedFlame, 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
