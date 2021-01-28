using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class MotherboardHelmet : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Motherboard Helmet");
			Tooltip.SetDefault("Increases your base click radius by 20%");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 20000;
			Item.rare = 3;
			Item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<ClickerPlayer>().clickerRadius += 0.4f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MotherboardSuit>() && legs.type == ModContent.ItemType<MotherboardBoots>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Right click to place a radius extending sensor";
			player.GetModPlayer<ClickerPlayer>().setMotherboard = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup("ClickerClass:SilverBar", 15);
			recipe.AddIngredient(ItemID.Wire, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}