using ClickerClass.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Buffs
{
	public class HoneySlow : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Honey");
			Description.SetDefault("Movement speed significantly reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			LongerExpertDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<ClickerGlobalNPC>().honeySlow = true;
		}
	}
}
