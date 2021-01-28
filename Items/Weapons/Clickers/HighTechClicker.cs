using Microsoft.Xna.Framework;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class HighTechClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("High Tech Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(75, 255, 200, 0));
			SetDust(Item, 226);
			SetAmount(Item, 10);
			SetEffect(Item, "Discharge");


			Item.damage = 90;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 500000;
			Item.rare = 8;
		}
	}
}
