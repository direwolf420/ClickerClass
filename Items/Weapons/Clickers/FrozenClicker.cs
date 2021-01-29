using ClickerClass.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class FrozenClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Frozen Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(175, 255, 255, 0));
			SetDust(Item, 15);
			SetAmount(Item, 1);
			SetEffect(Item, "Freeze");


			Item.damage = 82;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 500000;
			Item.rare = 8;
			Item.shoot = ModContent.ProjectileType<FrozenClickerPro>();
		}
	}
}
