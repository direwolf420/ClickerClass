using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class OverclockHelmet : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Overclock Helmet");
			Tooltip.SetDefault("Reduces the amount of clicks required for a click effect by 1");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 30000;
			Item.rare = 6;
			Item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerBonus += 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<OverclockSuit>() && legs.type == ModContent.ItemType<OverclockBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Every 100 clicks briefly grants you 'Overclock', making every click trigger its effect";
			player.GetModPlayer<ClickerPlayer>().setOverclock = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 14);
			recipe.AddIngredient(ItemID.SoulofSight, 6);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}