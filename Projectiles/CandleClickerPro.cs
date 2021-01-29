using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class CandleClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 6;
		}

		public override void AI()
		{
			if (Projectile.ai[0] > 0f)
			{
				Projectile.extraUpdates = 0;
				for (int k = 0; k < 2; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 55, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 255, default, 0.75f);
					dust.noGravity = true;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.ai[0] < 1f)
			{
				Projectile.timeLeft = 180;
				Projectile.velocity = Vector2.Zero;
				Projectile.ai[0] = 1f;
			}
			return false;
		}
	}
}