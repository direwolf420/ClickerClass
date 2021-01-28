using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class OrichaclumClickerPro : ClickerProjectile
	{
		public int timer = 0;
		public float rotation = 0f;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			Main.projFrames[Projectile.type] = 3;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 240;
			Projectile.extraUpdates = 3;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 0) * (0.01f * Projectile.timeLeft);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Rectangle frame = new Rectangle(0, 0, 20, 20);
			frame.Y += 20 * Projectile.frame;

			var asset = TextureAssets.Projectile[Projectile.type];
			Vector2 drawOrigin = new Vector2(asset.Width() * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(new Color(255, 255, 255, 0) * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				spriteBatch.Draw(asset.Value, drawPos, frame, color * (0.0025f * Projectile.timeLeft), rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
		{
			Projectile.frame = (int)(Projectile.ai[0]);
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.2f : -0.2f;

			timer++;
			if (timer % 5 == 0)
			{
				rotation = Projectile.rotation;
			}

			if (Projectile.ai[1] < 1f)
			{
				Projectile.ai[1] += 1f;

				float num102 = 30f;
				int num103 = 0;
				while ((float)num103 < num102)
				{
					Vector2 vector12 = Vector2.UnitX * 0f;
					vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(2f, 2f);
					vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int num104 = Dust.NewDust(Projectile.Center, 0, 0, 86, 0f, 0f, 0, default(Color), 1.25f);
					Main.dust[num104].noGravity = true;
					Main.dust[num104].position = Projectile.Center + vector12;
					Main.dust[num104].velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 2f;
					int num = num103;
					num103 = num + 1;
				}
			}
		}
	}
}