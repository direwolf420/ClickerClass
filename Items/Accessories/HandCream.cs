using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class HandCream : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Pressing the 'Clicker Accessory' key will toggle auto click on all Clickers"
							+ "\nWhile auto click is enabled, click rates are decreased");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 50000;
			Item.rare = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accHandCream = true;
		}
	}
}
