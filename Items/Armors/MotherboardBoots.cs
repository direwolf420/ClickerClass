using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Legs)]
	public class MotherboardBoots : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Motherboard Boots");
			Tooltip.SetDefault("Increases click damage by 6%");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 25000;
			Item.rare = 3;
			Item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerDamage += 0.06f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup("ClickerClass:SilverBar", 20);
			recipe.AddIngredient(ItemID.Wire, 50);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}