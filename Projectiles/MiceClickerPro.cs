using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class MiceClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			Projectile.alpha = 255;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 1;
			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			Projectile.ai[0]++;
			if (Projectile.ai[0] > 2)
			{
				Projectile.velocity.Y += Main.rand.NextFloat(-1f, 1f);
				Projectile.velocity.X += Main.rand.NextFloat(-1f, 1f);
				Projectile.ai[0] = 0;
			}

			for (int num363 = 0; num363 < 1; num363++)
			{
				float num364 = Projectile.velocity.X / 3f * (float)num363;
				float num365 = Projectile.velocity.Y / 3f * (float)num363;
				int num366 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<MiceDust>(), 0f, 0f, 0, default(Color), 1f);
				Main.dust[num366].position.X = Projectile.Center.X - num364;
				Main.dust[num366].position.Y = Projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}

			if (Projectile.timeLeft < 540)
			{
				Projectile.friendly = true;
				Projectile.extraUpdates = 4;

				if (Main.myPlayer == Projectile.owner)
				{
					float num477 = Projectile.Center.X;
					float num478 = Projectile.Center.Y;
					float num479 = 1000f;
					bool flag17 = false;

					float num481 = Main.MouseWorld.X;
					float num482 = Main.MouseWorld.Y;
					float num483 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num481) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num482);
					if (num483 < num479)
					{
						num479 = num483;
						num477 = num481;
						num478 = num482;
						flag17 = true;
					}

					if (flag17)
					{
						float num488 = 3f;

						Vector2 vector38 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
						float num489 = num477 - vector38.X;
						float num490 = num478 - vector38.Y;
						float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
						num491 = num488 / num491;
						num489 *= num491;
						num490 *= num491;

						Projectile.velocity.X = (Projectile.velocity.X * 20f + num489) / 21f;
						Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num490) / 21f;
					}
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default, 1f);
				dust.noGravity = true;
			}
		}
	}
}