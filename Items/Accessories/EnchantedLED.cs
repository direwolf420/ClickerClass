using Terraria;

namespace ClickerClass.Items.Accessories
{
	public class EnchantedLED : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Enchanted LED");
			Tooltip.SetDefault("Your clicks produce an enchanted burst of light, while accessory is visible"
							+ "\nIncreases click damage by 2");
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
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (!hideVisual)
			{
				clickerPlayer.accEnchantedLED = true;
			}
			clickerPlayer.clickerDamageFlat += 2;
		}
	}
}
