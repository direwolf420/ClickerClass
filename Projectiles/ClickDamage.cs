using ClickerClass.Items;
using Terraria;

namespace ClickerClass.Projectiles
{
	/// <summary>
	/// The most common damage-inflicting clicker projectile spawned from a clicker. Default for a clicker weapon
	/// </summary>
	public class ClickDamage : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[Projectile.owner];
			Item item = player.HeldItem;

			int dustType = 0;
			if (ClickerSystem.IsClickerWeapon(item))
			{
				ClickerItemCore clickerItem = item.GetGlobalItem<ClickerItemCore>();
				dustType = clickerItem.clickerDustColor;
			}

			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, dustType, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 75, default, 1f);
				dust.noGravity = true;
			}

			ClickerPlayer clickerPlayer = player.GetModPlayer<ClickerPlayer>();
			if (clickerPlayer.accEnchantedLED2)
			{
				for (int k = 0; k < 5; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 90, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.15f);
					dust.noGravity = true;
				}
			}

			if (clickerPlayer.accEnchantedLED)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustType1 = Main.rand.Next(3);
					switch (dustType1)
					{
						case 0: dustType1 = 15; break;
						case 1: dustType1 = 57; break;
						default: dustType1 = 58; break;
					}
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, dustType1, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 100, default, 1.5f);
					dust.velocity *= 1.5f;
					dust.noGravity = true;
				}
			}
		}
	}
}