using ClickerClass.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClass.Projectiles
{
	public class BoneClickerPro : ClickerProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 10;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Gouge>(), 60, false);
		}
	}
}