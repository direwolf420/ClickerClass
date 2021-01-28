using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class ShadowyClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Shadowy Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 2.15f);
			SetColor(Item, new Color(150, 100, 255, 0));
			SetDust(Item, 27);
			SetAmount(Item, 12);
			SetEffect(Item, "Curse");


			Item.damage = 12;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 15000;
			Item.rare = 1;
		}
	}
}
