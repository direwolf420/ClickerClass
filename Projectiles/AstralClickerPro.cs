using ClickerClass.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class AstralClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 120;
			Projectile.tileCollide = false;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				return new Color(255, 255, 255, 0) * 0.6f;
			}
			else
			{
				return new Color(255, 255, 255, 0) * 0f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				spriteBatch.Draw(Mod.GetTexture("Projectiles/AstralClickerPro").Value, Projectile.Center - Main.screenPosition, null, new Color(255, 255, 255, 0) * 0.25f, Projectile.ai[0], new Vector2(30, 30), 1.25f, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			Projectile.rotation += 0.065f;
			Projectile.ai[0] -= 0.15f;

			for (int k = 0; k < 1; k++)
			{
				Vector2 offset = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
				Dust dust = Dust.NewDustDirect(new Vector2(Projectile.Center.X - 10, Projectile.Center.Y - 10) + offset, 20, 20, ModContent.DustType<MiceDust>(), Scale: 1.5f);
				dust.noGravity = true;
				dust.velocity = -offset * 0.1f;
			}

			for (int u = 0; u < 200; u++)
			{
				NPC target = Main.npc[u];

				if (target.active && (target.type < 548 || target.type > 578) && target.type != NPCID.TargetDummy && !target.friendly && !target.boss && target.CanBeChasedBy(Projectile, false) && Vector2.Distance(Projectile.Center, target.Center) < 150)
				{
					float num3 = 11f;
					Vector2 vector = new Vector2(target.position.X + (float)(target.width / 2), target.position.Y + (float)(target.height / 2));
					float num4 = Projectile.Center.X - vector.X;
					float num5 = Projectile.Center.Y - vector.Y;
					float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
					num6 = num3 / num6;
					num4 *= num6;
					num5 *= num6;
					int num7 = 5;
					target.velocity.X = (target.velocity.X * (float)(num7 - 1) + num4) / (float)num7;
					target.velocity.Y = (target.velocity.Y * (float)(num7 - 1) + num5) / (float)num7;
				}
			}

			if (Projectile.timeLeft == 4)
			{
				SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 122);

				for (int k = 0; k < 30; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, ModContent.DustType<MiceDust>(), Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f), 0, default, 1.5f);
					dust.noGravity = true;
				}
				for (int k = 0; k < 20; k++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, 88, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f), 125, default, 1.15f);
					dust.noGravity = true;
				}
			}

			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
			{
				Projectile.velocity.X = 0f;
				Projectile.velocity.Y = 0f;
				Projectile.tileCollide = false;
				Projectile.friendly = true;
				Projectile.alpha = 255;
				Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
				Projectile.width = 200;
				Projectile.height = 200;
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			}
		}
	}
}