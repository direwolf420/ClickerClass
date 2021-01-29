using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CaptainsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Captain's Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.15f);
			SetColor(Item, new Color(255, 225, 50, 0));
			SetDust(Item, 10);
			SetAmount(Item, 12);
			SetEffect(Item, "Bombard");


			Item.damage = 30;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 180000;
			Item.rare = 4;
		}
	}
}
