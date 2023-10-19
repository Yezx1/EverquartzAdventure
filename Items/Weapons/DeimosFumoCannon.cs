using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using EverquartzAdventure.Tiles;
using EverquartzAdventure.Projectiles;

namespace EverquartzAdventure.Items.Weapons
{
    public class DeimosFumoCannon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 40;
            Item.rare = ModContent.RarityType<CelestialRarity>();
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.useTime = 15;
            Item.value = 0;
            Item.scale = 1.25f;
            Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<DeimosFumoThrown>();
            Item.useAmmo = ModContent.ItemType<DeimosFumo>();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12f, -3f);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.UseSound = SoundID.Item38;
                Item.useTime = 60;
                Item.useAnimation = 60;
            }
            else
            {
                Item.UseSound = SoundID.Item61;
                Item.useTime = 15;
                Item.useAnimation = 15;
            }
            return base.CanUseItem(player);
        }
        //example modm spread projectiles code
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            
            if (player.altFunctionUse == 2)
            {
                
                float numberProjectiles = 5; // 3, 4, or 5 shots
                float rotation = MathHelper.ToRadians(45);

                position += Vector2.Normalize(velocity) * 45f;

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.5f; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
                }
                return false; // return false to stop vanilla from calling Projectile.NewProjectile.
            }
            else
            {
                return true;
            }
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) 
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
                position += muzzleOffset;
            }
        }

        public override void OnConsumeAmmo(Item ammo, Player player)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (!player.ConsumeItem(ammo.type))
                    {
                        break;
                    }
                }
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}