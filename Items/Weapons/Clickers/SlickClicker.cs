using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class SlickClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Slick Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.45f);
			SetColor(Item, new Color(75, 75, 255, 0));
			SetDust(Item, 33);
			SetAmount(Item, 6);
			SetEffect(Item, "Splash");


			Item.damage = 11;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 87500;
			Item.rare = 2;
		}
	}
}
