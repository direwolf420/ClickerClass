using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class CookiePro : ClickerProjectile
	{
		public Vector2 location = Vector2.Zero;
		
		public override void SetDefaults()
		{
			isClickerProj = true;
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 2;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (projectile.timeLeft > 150)
			{
				return new Color(255, 255, 255, 200) * (0.005f * (int)(300 - projectile.timeLeft));
			}
			else
			{
				return new Color(255, 255, 255, 200) * (0.005f * projectile.timeLeft);
			}
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.frame = (int)(projectile.ai[0]);
			projectile.rotation -= 0.01f;
			
			if (projectile.ai[1] < 1f)
			{
				location.X = player.Center.X - (projectile.Center.X - 15);
				location.Y = player.Center.Y - (projectile.Center.Y - 15);
				projectile.ai[1] += 1f;
			}
			
			projectile.position = player.Center - location;
		}
	}
}