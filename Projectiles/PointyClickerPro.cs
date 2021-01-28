using ClickerClass.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class PointyClickerPro : ClickerProjectile
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.timeLeft = 120;
			Projectile.extraUpdates = 2;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 60;
		}

		public override void AI()
		{
			if (Projectile.localAI[0] < 48)
			{
				Projectile.localAI[0]++;
			}

			if (Projectile.ai[1] > 0f)
			{
				Projectile.SineWaveMovement(Projectile.ai[0], 10f, MathHelper.TwoPi / 40, Projectile.ai[0] == 0);
			}
			else
			{
				Projectile.SineWaveMovement(Projectile.ai[0], -10f, MathHelper.TwoPi / 40, Projectile.ai[0] == 0);
			}
			Projectile.ai[0]++;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var asset = TextureAssets.Projectile[Projectile.type];
			Vector2 drawOrigin = new Vector2(asset.Width() * 0.5f, Projectile.height * 0.5f);
			for (int i = Projectile.oldPos.Length - 1; i >= 0; i--)
			{
				Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor * 0.25f) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
				spriteBatch.Draw(asset.Value, drawPos, null, color, Projectile.oldRot[i], drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}