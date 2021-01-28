using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Body)]
	public class PrecursorBreastplate : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Precursor Breastplate");
			Tooltip.SetDefault("Increases click damage by 10%"
							+ "\nReduces base clicker radius by 50%");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 80000;
			Item.rare = 8;
			Item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			clickerPlayer.clickerDamage += 0.1f;
			clickerPlayer.clickerRadius -= 1f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarTabletFragment, 18);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}