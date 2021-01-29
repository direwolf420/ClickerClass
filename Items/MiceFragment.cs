using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Items
{
	public class MiceFragment : ClickerItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mice Fragment");
			Tooltip.SetDefault("'Erratic sparks skitter across this fragment'");
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = 9;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
	}
}