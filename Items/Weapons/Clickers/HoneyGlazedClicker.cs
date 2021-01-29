using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HoneyGlazedClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Honey Glazed Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.45f);
			SetColor(Item, new Color(255, 175, 0, 0));
			SetDust(Item, 153);
			SetAmount(Item, 1);
			SetEffect(Item, "Sticky Honey");


			Item.damage = 13;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 10000;
			Item.rare = 3;
			Item.shoot = ModContent.ProjectileType<HoneyGlazedClickerPro>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BeeWax, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
