using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class CorruptClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 250;
			Projectile.height = 250;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 3;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Oiled, 180, false);
			target.AddBuff(BuffID.CursedInferno, 180, false);

			for (int i = 0; i < 15; i++)
			{
				int num6 = Dust.NewDust(target.position, target.width, target.height, 163, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[num6].noGravity = true;
				Main.dust[num6].velocity *= 0.75f;
				int num7 = Main.rand.Next(-50, 51);
				int num8 = Main.rand.Next(-50, 51);
				Dust dust = Main.dust[num6];
				dust.position.X = dust.position.X + (float)num7;
				Dust dust2 = Main.dust[num6];
				dust2.position.Y = dust2.position.Y + (float)num8;
				Main.dust[num6].velocity.X = -(float)num7 * 0.075f;
				Main.dust[num6].velocity.Y = -(float)num8 * 0.075f;
			}
		}
	}
}