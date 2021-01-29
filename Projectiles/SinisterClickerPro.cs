using Terraria;

namespace ClickerClass.Projectiles
{
	public class SinisterClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 10;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.extraUpdates = 3;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
			player.statLife += 5;
			player.HealEffect(5);
		}
	}
}