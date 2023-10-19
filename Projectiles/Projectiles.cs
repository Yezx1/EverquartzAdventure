using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using Terraria.ModLoader;

namespace EverquartzAdventure
{
    internal static partial class CalamityWeakRef
    {
        [JITWhenModsEnabled("Calamitymod")]
        internal static class ProjectileType
        {
            public static int TelluricGlareArrow => ModContent.ProjectileType<TelluricGlareArrow>();
            public static int DWArrow => ModContent.ProjectileType<DWArrow>();
            public static int DeathhailBeam => ModContent.ProjectileType<DeathhailBeam>();
            public static int HolyLaser => ModContent.ProjectileType<HolyLaser>();
            public static int VenusianTrident => ModContent.ProjectileType<PrinceFlameLarge>();
            public static int PrinceFlameSmall => ModContent.ProjectileType<PrinceFlameSmall>();
            public static int HolyColliderHolyFire => ModContent.ProjectileType<HolyColliderHolyFire>();
            public static int GalaxyStar => ModContent.ProjectileType<EssenceFlame2>();
            public static int AuroraFire => ModContent.ProjectileType<AuroraFire>();
        }
    }
}