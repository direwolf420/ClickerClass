using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class HemoClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void AI()
		{
			if (Projectile.timeLeft < 280)
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
				int num366 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 183, 0f, 0f, 75, default(Color), 1.35f);
				Main.dust[num366].position.X = Projectile.Center.X - num364;
				Main.dust[num366].position.Y = Projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}