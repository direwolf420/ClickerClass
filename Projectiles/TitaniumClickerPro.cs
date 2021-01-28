using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ClickerClass.Projectiles
{
	public class TitaniumClickerPro : ClickerProjectile
	{
		public int radiusIncrease = 0;
		public int timer = 0;
		public float rotation = 0f;
		public Vector2 center = Vector2.Zero;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
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
			Projectile.extraUpdates = 1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 20;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var asset = TextureAssets.Projectile[Projectile.type];
			Vector2 drawOrigin = new Vector2(asset.Width() * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor * 0.25f) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				spriteBatch.Draw(asset.Value, drawPos, null, color * (0.0025f * Projectile.timeLeft), rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
		{
			float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
			float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
			return new Vector2(newPosX, newPosY);
		}

		public float rot = 0f;

		public override void AI()
		{
			if (center == Vector2.Zero)
			{
				center = Projectile.Center;
			}
			Projectile.rotation += Projectile.velocity.X > 0f ? 0.2f : -0.2f;

			timer++;
			if (timer % 5 == 0)
			{
				rotation = Projectile.rotation;
			}

			radiusIncrease += 1;
			rot += 0.05f;
			Projectile.Center = center + RotateVector(default(Vector2), new Vector2(0, 20 + radiusIncrease), rot + (Projectile.ai[0] * (6.28f / 5)));
		}
	}
}