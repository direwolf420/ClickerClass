using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class StickyKeychain : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Every 10 clicks sticks damaging slime on to your screen");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 25000;
			Item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accStickyKeychain = true;
		}
	}
}
