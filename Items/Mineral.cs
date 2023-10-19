using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EverquartzAdventure.NPCs.TownNPCs;
using CalamityMod.Items;
using CalamityMod;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace EverquartzAdventure.Items
{
    public class MineralEquipped : ModPlayer
    {
        public bool hasMineral;
        public static MineralEquipped ModPlayer(Player player)
        {
            return player.GetModPlayer<MineralEquipped>();
        }
        public override void ResetEffects()
        {
            Reset();
        }

        public override void UpdateDead()
        {
            Reset();
        }

        private void Reset()
        {
            hasMineral = false;
        }
    }
    [AutoloadEquip(EquipType.Wings)]
    public class Mineral : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(5184000, 15f, 2.5f);
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.scale = 0.75f;
            Item.maxStack = 1;
            Item.value = 0;
            Item.defense = 75;
            Item.accessory = true;
            if (ModCompatibility.calamityEnabled)
            {
                Item.rare = ModContent.RarityType<CalamityRed>();
            }
            else
            {
                Item.rare = ModContent.RarityType<CelestialRarity>();
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MineralEquipped modPlayer = MineralEquipped.ModPlayer(player);
            player.GetModPlayer<MineralEquipped>().hasMineral = true;
            player.GetDamage(damageClass: DamageClass.Generic) += 0.55f;
            player.GetCritChance(damageClass: DamageClass.Generic) += 55f;
            player.endurance += 0.35f;
            player.lifeRegen += 50;
            player.wingTime = player.wingTimeMax;
            player.lifeMagnet = true;
            player.manaMagnet = true;
            player.treasureMagnet = true;
            player.maxMinions += 10;
            player.maxTurrets += 10;
            player.breath = player.breathMax;
            player.statLifeMax2 += 400;
            player.manaFlower = true;
            player.statManaMax2 += 600;
            player.moveSpeed *= 2.5f;
            player.GetArmorPenetration(damageClass: DamageClass.Generic) += 50f;
            player.luck = 1f;
            player.coinLuck = 1000000f;
            player.runAcceleration *= 1.75f;
            player.autoJump = true;
            player.accFlipper = true;
            //player.buffImmune[BuffID.ChaosState] = true;
            /*ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);
            if (calamityMod != null)
            {
                calamityMod.Call("ToggleInfiniteFlight", player, () => player.GetModPlayer<MineralEquipped>().hasMineral);
            }*/ //idk 
        }
    }
}