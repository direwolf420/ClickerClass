using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Items.Weapons.Clickers
{
	public class LordsClicker : ClickerWeapon
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Lord's Clicker");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			SetRadius(Item, 6f);
			SetColor(Item, new Color(100, 255, 200, 0));
			SetDust(Item, 110);
			SetAmount(Item, 15);
			SetEffect(Item, "Conqueror");


			Item.damage = 122;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = 10;
		}
	}
}
