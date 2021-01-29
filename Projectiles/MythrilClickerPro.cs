using ClickerClass.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class MythrilClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.alpha = 255;
			Projectile.extraUpdates = 3;
		}

		public override void AI()
		{
			if (Projectile.ai[0] < 1f)
			{
				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 57, Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.35f);
					dust.shader = GameShaders.Armor.GetSecondaryShader(20, Main.LocalPlayer);
					dust.noGravity = true;
				}
				for (int u = 0; u < Main.maxNPCs; u++)
				{
					NPC target = Main.npc[u];
					if (target.CanBeChasedBy(Projectile) && target.DistanceSQ(Projectile.Center) < 250 * 250)
					{
						target.AddBuff(ModContent.BuffType<Embrittle>(), 240, false);

						for (int i = 0; i < 15; i++)
						{
							int num6 = Dust.NewDust(target.position, target.width, target.height, 57, 0f, 0f, 100, default(Color), 1f);
							Main.dust[num6].shader = GameShaders.Armor.GetSecondaryShader(20, Main.LocalPlayer);
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
				Projectile.ai[0] += 1f;
			}
		}
	}
}