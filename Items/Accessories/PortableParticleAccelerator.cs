using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Items.Accessories
{
	public class PortableParticleAccelerator : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Tooltip.SetDefault("Clicking within the inner 20% of your clicker radius deals 20% more damage");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = 45000;
			Item.rare = -1;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<ClickerPlayer>().accPortableParticleAccelerator = true;
		}

		/*
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HallowedBar, 12);
			recipe.AddIngredient(ItemID.Cog, 10);
			recipe.AddIngredient(ItemID.Wire, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
		*/
	}
}
