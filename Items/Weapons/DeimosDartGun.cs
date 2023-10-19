using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Rarities;
using Terraria.DataStructures;
using EverquartzAdventure.Tiles;

namespace EverquartzAdventure.Items.Weapons
{
    public class DeimosDartGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 28;
            Item.UseSound = SoundID.Item99;
            if (ModCompatibility.calamityEnabled)
            {
                Item.rare = ModContent.RarityType<PureGreen>();
            }
            else
            {
                Item.rare = ModContent.RarityType<CelestialRarity>();
            }
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 390;
            Item.value = Item.buyPrice(1, 30, 0, 0);
            Item.knockBack = 2.5f;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Dart;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0f, 0f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.CursedDart)
            {
                type = ModContent.ProjectileType<DeiNightwitherDart>();
            }
            /*else if (type == ProjectileID.PoisonDartBlowgun)
            {
                type = ModContent.ProjectileType<DeiSicknessDart>();
            }*/
            else if (type == ProjectileID.CrystalDart)
            {
                type = ModContent.ProjectileType<DeiGodSlayerDart>();
            }
            else if (type == ProjectileID.IchorDart)
            {
                type = ModContent.ProjectileType<DeiHolyFlamesDart>();
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<DeiNightwitherDart>())
            {
                SoundEngine.PlaySound(SoundID.Item105, position);
            }
            else if (type == ModContent.ProjectileType<DeiGodSlayerDart>())
            {
                SoundEngine.PlaySound(SoundID.Item12, position);
            }
            else if (type == ModContent.ProjectileType<DeiHolyFlamesDart>())
            {
                SoundEngine.PlaySound(SoundID.Item60, position);
            }
            return true;
            /*if (type == ModContent.ProjectileType<DeiNightwitherDart>())
            {
                float projNum = 5;
                float projRot = MathHelper.ToRadians(45);
                position += Vector2.Normalize(velocity) * 45f;
                for (int i = 0; i < projNum; i++)
                {
                    Vector2 DNWDspeed = velocity.RotatedBy(MathHelper.Lerp(-projRot, projRot, i / (projNum - 1) * .2f));
                    Projectile.NewProjectile(source, position, DNWDspeed, type, damage, knockback, player.whoAmI);
                }
                return false;
            }
            else 
            {*/
            //return true;
            //}
            }
        }

    /*public class DeiSicknessDart : ModProjectile
    {
        private Color DustColor = Color.OrangeRed;
        private Color DustColor2 = Color.Teal;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PoisonDartBlowgun);
            AIType = ProjectileID.BookOfSkullsSkull;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
            DrawOriginOffsetY = -1;
            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
            }
            else
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor2, 1f);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
                }
                else
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor2, 1f);
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Zombie103, Projectile.position);
            if (ModCompatibility.calamityEnabled)
            {
                Vector2 auroraLaunch = new Vector2(10, 10);
                for (int i = 0; i < 4; i++)
                {
                    auroraLaunch = auroraLaunch.RotatedBy(MathHelper.PiOver4);
                    float dustLaunchX = auroraLaunch.X;
                    float dustLaunchY = auroraLaunch.Y;
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, auroraLaunch, CalamityWeakRef.ProjectileType.AuroraFire, Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, -auroraLaunch, CalamityWeakRef.ProjectileType.AuroraFire, Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, dustLaunchX, dustLaunchY, 175, DustColor, 0.75f);
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, -dustLaunchX, -dustLaunchY, 175, DustColor, 0.75f);
                }

            }
            for (int i = 0; i < 5; i++)
            {
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
                }
                else
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor2, 1f);
                }
            }
            Projectile.Kill();
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            //modifiers.SourceDamage *= 0.75f;
            if (ModCompatibility.calamityEnabled)
            {
                target.AddBuff(CalamityWeakRef.BuffType.AstralInfectionDebuff, 300);
            }
        }
    }*/

    public class DeiGodSlayerDart : ModProjectile
    {
        private Color DustColor = Color.Fuchsia;
        private Color DustColor2 = Color.DeepSkyBlue;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PoisonDartBlowgun);
            AIType = -1; //0 //ProjectileID.CrystalDart
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.penetrate = -1;
            Projectile.light = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 90;
            Projectile.velocity *= 1.35f; //new Vector2(7.5f, 7.5f)
            DrawOriginOffsetY = -1;
            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
            }
            else
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor2, 1f);
            }
            Projectile.velocity *= 1.015f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
                }
                else
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor2, 1f);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= 0.9f;
            if (ModCompatibility.calamityEnabled)
            {
                target.AddBuff(CalamityWeakRef.BuffType.GodSlayerInferno, 120);
            }
        }
    }

    public class DeiNightwitherDart : ModProjectile
    {
        private Color DustColor = Color.Cyan;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PoisonDartBlowgun);
            AIType = -1; //ProjectileID.CrystalDart
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 120;
            DrawOriginOffsetY = -1;
            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
            if (Projectile.velocity.Y <= 6f || Projectile.velocity.Y >= -6f)
            {
                float randVelNight = Main.rand.NextFloat(-2f, 2f);
                Projectile.velocity.Y += randVelNight;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= 2.0f;
            if (ModCompatibility.calamityEnabled)
            {
                target.AddBuff(CalamityWeakRef.BuffType.Nightwither, 240);
            }
        }
    }
    public class DeiHolyFlamesDart : ModProjectile
    {
        private Color DustColor = Color.LightGoldenrodYellow;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.PoisonDartBlowgun);
            AIType = ProjectileID.JestersArrow;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.penetrate = 1;
            Projectile.light = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 90;
            DrawOriginOffsetY = -1;
            DrawOffsetX = -4;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 25; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, DustColor, 1f);
            }
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            if (ModCompatibility.calamityEnabled)
            {
                Vector2 proviLaunch = new Vector2(4, 4);
                for (int i = 0; i < 2; i++)
                {
                    proviLaunch = proviLaunch.RotatedBy(MathHelper.PiOver2);
                    float dustLaunchX = proviLaunch.X;
                    float dustLaunchY = proviLaunch.Y;
                    int DamageMod = Projectile.damage / 4;
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, proviLaunch, CalamityWeakRef.ProjectileType.TelluricGlareArrow, DamageMod * 3, Projectile.knockBack, Projectile.owner);
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, -proviLaunch, CalamityWeakRef.ProjectileType.TelluricGlareArrow, DamageMod * 3, Projectile.knockBack, Projectile.owner);
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, dustLaunchX, dustLaunchY, 100, DustColor, 0.75f);
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, -dustLaunchX, -dustLaunchY, 100, DustColor, 0.75f);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (ModCompatibility.calamityEnabled)
            {
                target.AddBuff(CalamityWeakRef.BuffType.HolyFlames, 120);
            }
        }
    }
}