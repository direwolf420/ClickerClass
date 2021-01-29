using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class SlickClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 180;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 3;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			if (Projectile.timeLeft < 170)
			{
				Projectile.friendly = true;
			}

			Projectile.velocity.Y /= 1.0065f;

			Projectile.ai[1]++;

			if (Projectile.ai[1] >= 0)
			{
				Projectile.velocity.Y += 1.05f;
				Projectile.ai[1] = -15;
			}

			for (int num363 = 0; num363 < 3; num363++)
			{
				float num364 = Projectile.velocity.X / 3f * (float)num363;
				float num365 = Projectile.velocity.Y / 3f * (float)num363;
				int num366 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 33, 0f, 0f, 100, default(Color), 1.25f);
				Main.dust[num366].position.X = Projectile.Center.X - num364;
				Main.dust[num366].position.Y = Projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}

			for (int k = 0; k < 2; k++)
			{
				int dust = Dust.NewDust(Projectile.position, 10, 10, 33, Main.rand.Next((int)-1f, (int)1f), Main.rand.Next((int)-1f, (int)1f), 100, default(Color), 0.75f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}