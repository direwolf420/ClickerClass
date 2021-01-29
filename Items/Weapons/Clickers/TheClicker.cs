using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class TheClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("The Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(255, 255, 255, 0));
			SetDust(Item, 91);
			SetAmount(Item, 1);
			SetEffect(Item, "The Click");


			Item.damage = 150;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = 10;
		}
	}
}
