using Microsoft.Xna.Framework;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class StickyKeychainPro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 72;
			Projectile.height = 72;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 200) * (0.005f * Projectile.timeLeft);
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.frame = (int)Projectile.ai[0];

			if (Projectile.ai[1] < 1f)
			{
				location = player.Center - Projectile.Center;
				Projectile.ai[1] += 1f;
			}

			Projectile.Center = new Vector2(player.Center.X, player.Center.Y + player.gfxOffY) - location;
		}
	}
}