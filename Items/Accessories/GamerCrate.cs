using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	[AutoloadEquip(EquipType.Waist)]
	public class GamerCrate : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("'You don't actually think someone would play this, do you?'"
						+ "\nIncreases click damage by 10%"
						+ "\nIncreases your base click radius by 50%"
						+ "\nReduces the amount of clicks required for a click effect by 20%"
						+ "\nYour clicks produce a burst of mechanical light, while accessory is visible"
						+ "\nPressing the 'Clicker Accessory' key will toggle auto click on all Clickers"
						+ "\nWhile auto click is enabled, click rates are decreased");
		}

		public override void SetDefaults()
		{
			SetDisplayTotalClicks(Item);
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = 7;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerRadius += 1f;
			clickerPlayer.clickerDamage += 0.10f;
			clickerPlayer.clickerBonusPercent -= 0.20f;
			clickerPlayer.accHandCream = true;
			if (!hideVisual)
			{
				clickerPlayer.accEnchantedLED2 = true;
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<EnchantedLED>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Soda>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MousePad>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HandCream>(), 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}
	}
}
