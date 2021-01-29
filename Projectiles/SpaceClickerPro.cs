using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class SpaceClickerPro : ClickerProjectile
	{
		public int timer = 0;
		public float rotation = 0f;
		public bool colorChoice = false;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 7;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
			Projectile.extraUpdates = 2;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			if (Projectile.timeLeft > 4)
			{
				return new Color(255, 255, 255, 0) * 1f;
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
				Rectangle frame = new Rectangle(0, 0, 22, 26);
				frame.Y += 26 * Projectile.frame;

				var asset = TextureAssets.Projectile[Projectile.type];
				Vector2 drawOrigin = new Vector2(asset.Width() * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(new Color(255, 255, 255, 0) * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					spriteBatch.Draw(asset.Value, drawPos, frame, color * 0.25f, rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public override void AI()
		{
			if (!colorChoice)
			{
				Projectile.frame = Main.rand.Next(7);
				colorChoice = true;
			}
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.35f : -0.35f;

			timer++;
			if (timer % 5 == 0)
			{
				rotation = Projectile.rotation;
			}

			int dustType = 90;
			if (Projectile.frame == 1) { dustType = 92; }
			if (Projectile.frame == 2) { dustType = 87; }
			if (Projectile.frame == 3) { dustType = 89; }
			if (Projectile.frame == 4) { dustType = 92; }
			if (Projectile.frame == 5) { dustType = 88; }
			if (Projectile.frame == 6) { dustType = 86; }

			Vector2 vec = new Vector2(Projectile.ai[0], Projectile.ai[1]);
			if (Vector2.Distance(Projectile.Center, vec) <= 10)
			{
				if (Projectile.timeLeft > 4)
				{
					SoundEngine.PlaySound(2, (int)Projectile.Center.X, (int)Projectile.Center.Y, 110);
					Projectile.timeLeft = 4;

					float num102 = 30f;
					int num103 = 0;
					while ((float)num103 < num102)
					{
						Vector2 vector12 = Vector2.UnitX * 0f;
						vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(8f, 8f);
						vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
						int num104 = Dust.NewDust(Projectile.Center, 0, 0, dustType, 0f, 0f, 0, default(Color), 1.25f);
						if (Projectile.frame == 1) { Main.dust[num104].shader = GameShaders.Armor.GetSecondaryShader(70, Main.LocalPlayer); }
						Main.dust[num104].noGravity = true;
						Main.dust[num104].position = Projectile.Center + vector12;
						Main.dust[num104].velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 5f;
						int num = num103;
						num103 = num + 1;
					}
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
				Projectile.width = 150;
				Projectile.height = 150;
				Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
				Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			}
		}
	}
}