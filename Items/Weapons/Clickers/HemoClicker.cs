using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HemoClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Hemo Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.75f);
			SetColor(Item, new Color(255, 50, 50, 0));
			SetDust(Item, 5);
			SetAmount(Item, 10);
			SetEffect(Item, "Linger");


			Item.damage = 6;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 2f;
			Item.value = 13000;
			Item.rare = 1;
		}
	}
}
