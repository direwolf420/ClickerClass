using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class CookiePro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (Projectile.timeLeft > 150)
			{
				return new Color(255, 255, 255, 200) * (0.005f * (int)(300 - Projectile.timeLeft));
			}
			else
			{
				return new Color(255, 255, 255, 200) * (0.005f * Projectile.timeLeft);
			}
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.frame = (int)Projectile.ai[0];
			Projectile.rotation -= 0.01f;

			if (Projectile.ai[1] < 1f)
			{
				location = player.Center - Projectile.Center;
				Projectile.ai[1] += 1f;
			}

			Projectile.Center = new Vector2(player.Center.X, player.Center.Y + player.gfxOffY) - location;
		}
	}
}