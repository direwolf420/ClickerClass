using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace ClickerClass.Projectiles
{
	public class HighTechClickerPro : ClickerProjectile
	{
		List<int> targets = new List<int>();
		public int timer = 0;

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 100;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (targets.Contains(target.whoAmI))
			{
				return false;
			}
			return base.CanHitNPC(target);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.velocity = Vector2.Zero;
			targets.Add(target.whoAmI);
			if (targets.Count >= 8)
			{
				Projectile.Kill();
			}
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			for (int num363 = 0; num363 < 1; num363++)
			{
				float num364 = Projectile.velocity.X / 3f * (float)num363;
				float num365 = Projectile.velocity.Y / 3f * (float)num363;
				int num366 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0f, 0f, 0, default(Color), 1.15f);
				Main.dust[num366].position.X = Projectile.Center.X - num364;
				Main.dust[num366].position.Y = Projectile.Center.Y - num365;
				Main.dust[num366].velocity *= 0f;
				Main.dust[num366].noGravity = true;
			}

			if (Projectile.timeLeft < 580)
			{
				if (Projectile.timeLeft >= 600)
				{
					for (int k = 0; k < 6; k++)
					{
						int num367 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 125, default(Color), 1.15f);
						Main.dust[num367].noGravity = true;
					}
				}

				timer++;
				if (timer > 2)
				{
					Projectile.velocity.Y += Main.rand.NextFloat(-0.75f, 0.75f);
					Projectile.velocity.X += Main.rand.NextFloat(-0.75f, 0.75f);
					timer = 0;
				}

				int num3;
				float num477 = player.Center.X;
				float num478 = player.Center.Y;
				float num479 = 750f;
				bool flag17 = false;

				for (int num480 = 0; num480 < 200; num480 = num3 + 1)
				{
					if (Main.npc[num480].active && !targets.Contains(Main.npc[num480].whoAmI) && Main.npc[num480].CanBeChasedBy(Projectile, false) && Projectile.Distance(Main.npc[num480].Center) < num479 && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num480].Center, 1, 1))
					{
						float num481 = Main.npc[num480].position.X + (float)(Main.npc[num480].width / 2);
						float num482 = Main.npc[num480].position.Y + (float)(Main.npc[num480].height / 2);
						float num483 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num481) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num482);
						if (num483 < num479)
						{
							num479 = num483;
							num477 = num481;
							num478 = num482;
							flag17 = true;
						}
					}
					num3 = num480;
				}

				if (flag17)
				{
					float num488 = 2.5f;

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
				else
				{
					Projectile.velocity = Vector2.Zero;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, Main.rand.Next((int)-5f, (int)5f), Main.rand.Next((int)-5f, (int)5f), 75, default(Color), 0.75f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}