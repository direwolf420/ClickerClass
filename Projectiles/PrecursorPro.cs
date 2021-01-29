using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class PrecursorPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 40;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 300, false);
			target.AddBuff(BuffID.OnFire, 300, false);
		}

		public override void AI()
		{
			if (Projectile.timeLeft <= 10)
			{
				Projectile.friendly = true;
				if (Projectile.ai[0] < 1f)
				{
					Projectile.ai[0] += 1f;
					for (int k = 0; k < 8; k++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 174, Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1.25f);
						dust.noGravity = true;
					}
				}
			}
		}
	}
}