using Terraria;
using Terraria.Graphics.Shaders;

namespace ClickerClass.Projectiles
{
	public class TheClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[Projectile.owner];

			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 90, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 87, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 89, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 92, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 88, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 86, Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5f, 5f), 75, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}