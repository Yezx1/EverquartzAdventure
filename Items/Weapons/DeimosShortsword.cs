using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Weapons;
using CalamityMod.Items.Weapons.Melee;
using Terraria.Audio;
using Terraria.DataStructures;
using System;
using CalamityMod.Rarities;

namespace EverquartzAdventure.Items.Weapons
{
    public class DeimosShortsword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 4500;
            Item.knockBack = 0.5f;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useAnimation = 4;
            Item.useTime = 4;
            Item.width = 15;
            Item.height = 15;
            Item.UseSound = SoundID.Item1;
            if (ModCompatibility.calamityEnabled)
            {
                Item.DamageType = ModContent.GetModItem(ModContent.ItemType<MantisClaws>()).Item.DamageType;
            }
            else
            {
                Item.DamageType = DamageClass.MeleeNoSpeed;
            }
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            if (ModCompatibility.calamityEnabled)
            {
                Item.rare = ModContent.RarityType<DarkBlue>();
            }
            else
            {
                Item.rare = ModContent.RarityType<CelestialRarity>();
            }
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.shoot = ModContent.ProjectileType<DeimosShortswordShoot>();
            Item.shootSpeed = 2.1f;
        }
    }

    public class DeimosShortswordShoot : ModProjectile //I LOVE EXAMPLE MOD ! !!
    {
        public const int FadeInDuration = 7;
        public const int FadeOutDuration = 4;
        public const int TotalDuration = 16;
        public float CollisionWidth => 10f * Projectile.scale;
        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(20);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1.35f;
            if (ModCompatibility.calamityEnabled)
            {
                Projectile.DamageType = ModContent.GetModItem(ModContent.ItemType<DeimosShortsword>()).Item.DamageType;
            }
            else
            {
                Projectile.DamageType = DamageClass.Melee;
            }
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 360;
            Projectile.hide = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Timer += 1;
            if (Timer >= TotalDuration)
            {
                Projectile.Kill();
                return;
            }
            else
            {
                player.heldProj = Projectile.whoAmI;
            }
            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);
            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
            SetVisualOffsets();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item96, Projectile.position);
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, Color.LavenderBlush, 0.5f);
            }
            /*if (ModCompatibility.calamityEnabled)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Main.rand.NextFloat(-1.25f, 1.25f), -8f, CalamityWeakRef.ProjectileType.HolyColliderHolyFire, (int)(Projectile.damage * 3), 0f, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Main.rand.NextFloat(-1.25f, 1.25f), 5f, CalamityWeakRef.ProjectileType.GalaxyStar, (int)(Projectile.damage * 3), 0f, Projectile.owner, 0f, 0f);
            }*/
        }

        private void SetVisualOffsets()
        {
            const int HalfSpriteWidth = 30 / 2;
            const int HalfSpriteHeight = 30 / 2;
            int HalfProjWidth = Projectile.width / 2;
            int HalfProjHeight = Projectile.height / 2;
            if (Projectile.spriteDirection == 1)
            {
                DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth);
                DrawOffsetX = (int)-DrawOriginOffsetX * 2;
                DrawOriginOffsetY = 0;
            }
            else
            {
                DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth);
                DrawOffsetX = 0;
                DrawOriginOffsetY = 0;
            }
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
            Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity * 6f;
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
        }
    }

    /*public class DeimosFireProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DiamondBolt);
            Projectile.scale = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
            Projectile.ignoreWater = true;

        }

        public override void AI()
        {
            if (++Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
            Projectile.rotation = Projectile.velocity.ToRotation();

            Vector2 center = base.Projectile.Center;
            float homingRange = 300f;
            bool homeIn = false;
            float inertia = 25f;
            float homingSpeed = 10f;

            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(base.Projectile))
                {
                    float extraDistance = (float)(Main.npc[i].width / 2) + (float)(Main.npc[i].height / 2);
                    if (Vector2.Distance(Main.npc[i].Center, base.Projectile.Center) < homingRange + extraDistance)
                    {
                        center = Main.npc[i].Center;
                        homeIn = true;
                        break;
                    }
                }
            }
            if (homeIn)
            {
                base.Projectile.extraUpdates = 1;
                Vector2 homeInVector = (center - Projectile.Center).SafeNormalize(Vector2.UnitY);
                base.Projectile.velocity = (base.Projectile.velocity * inertia + homeInVector * homingSpeed) / (inertia + 1f);
            }
            else
            {
                base.Projectile.extraUpdates = 0;
            }
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 58, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (ModCompatibility.calamityEnabled)
            {
                target.AddBuff(CalamityWeakRef.BuffType.GodSlayerInferno, 60);
                target.AddBuff(CalamityWeakRef.BuffType.HolyFlames, 60);
            }

        }
    }*/
}