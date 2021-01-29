using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class ChlorophyteClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.aiStyle = -1;
			Projectile.alpha = 150;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 45;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 300, false);
			target.AddBuff(BuffID.Venom, 300, false);
		}

		public override void AI()
		{
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.1f : -0.1f;
			Projectile.velocity *= 0.95f;
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 5;
			}
		}
	}
}