using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class Cookie : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("While equipped, cookies will periodically spawn within your clicker radius"
							+ "\nClick the cookie to gain bonus clicker damage, radius, and life regeneration");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 20000;
			Item.rare = 1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accCookie = true;
		}
	}
}
