using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class BoneClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Bone Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.1f);
			SetColor(Item, new Color(225, 225, 200, 0));
			SetDust(Item, 216);
			SetAmount(Item, 12);
			SetEffect(Item, "Lacerate");


			Item.damage = 13;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.noMelee = true;
			Item.value = 15000;
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FossilOre, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
