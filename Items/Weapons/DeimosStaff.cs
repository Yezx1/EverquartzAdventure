﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Rarities;
using CalamityMod.Projectiles.Magic;

namespace EverquartzAdventure
{
    internal static partial class CalamityWeakRef
    {
        
    }
}

namespace EverquartzAdventure.Items.Weapons
{
    public class DeimosStaff : ModItem
    {
        //public override string Texture => "EverquartzAdventure/NPCs/TownNPCs/StarbornPrincess_Head";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Starfury");
            //DisplayName.AddTranslation(7, "宇宙星怒");
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 78;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 20;
            Item.scale = 0.8f;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 50;
            Item.knockBack = 6;
            Item.crit = 6;
            Item.mana = 17;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.UseSound = SoundID.Item43;
            if (ModCompatibility.calamityEnabled)
            {
                Item.rare = ModContent.RarityType<Turquoise>();
            }
            else
            {
                Item.rare = ModContent.RarityType<CelestialRarity>();
            }
            if (ModCompatibility.calamityEnabled)
            {
                Item.shoot = CalamityWeakRef.ProjectileType.DeathhailBeam;
            }

            Item.shootSpeed = 8f;
        }

        public override bool CanShoot(Player player)
        {
            return ModCompatibility.calamityEnabled;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, Utils.RotatedBy(velocity, -0.02500000037252903, new Vector2()), CalamityWeakRef.ProjectileType.HolyLaser, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity, CalamityWeakRef.ProjectileType.HolyLaser, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, Utils.RotatedBy(velocity, 0.02500000037252903, new Vector2()), CalamityWeakRef.ProjectileType.HolyLaser, damage, knockback, player.whoAmI);
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }

            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }


            return false;
        }


    }
}