using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class EclipticClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Ecliptic Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 3.5f);
			SetColor(Item, new Color(255, 200, 100, 0));
			SetDust(Item, 264);
			SetAmount(Item, 15);
			SetEffect(Item, "Totality");


			Item.damage = 48;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 165000;
			Item.rare = 6;
		}
	}
}
