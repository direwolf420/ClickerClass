using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class CandleClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Candle Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 1.5f);
			SetColor(Item, new Color(255, 175, 75, 0));
			SetDust(Item, 55);
			SetAmount(Item, 10);
			SetEffect(Item, "Illuminate");


			Item.damage = 8;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 25000;
			Item.rare = 2;
		}
	}
}
