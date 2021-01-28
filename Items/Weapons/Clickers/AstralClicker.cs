using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class AstralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Astral Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(150, 150, 225, 0));
			SetDust(Item, ModContent.DustType<MiceDust>());
			SetAmount(Item, 15);
			SetEffect(Item, "Spiral");


			Item.damage = 82;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = 10;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<MiceFragment>(), 18);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
