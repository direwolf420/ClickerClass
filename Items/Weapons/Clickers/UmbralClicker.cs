using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class UmbralClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Umbral Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.75f);
			SetColor(Item, new Color(150, 100, 255, 0));
			SetDust(Item, 27);
			SetAmount(Item, 10);
			SetEffect(Item, "Shadow Lash");


			Item.damage = 20;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 200000;
			Item.rare = 3;
		}

		/*
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DarkClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlickClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PointyClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RedHotClicker>(), 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SinisterClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SlickClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PointyClicker>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RedHotClicker>(), 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.Register();
		}
		*/
	}
}
