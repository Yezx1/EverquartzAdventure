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

namespace EverquartzAdventure.Items
{
    public class Terrideimo : ModPlayer
    {
        public bool canHurtDeimos;
        public override void ResetEffects()
        {
            canHurtDeimos = false;
        }
    }
    public class DeimosVoodooDoll : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ClothierVoodooDoll);
            Item.width = 36;
            Item.height = 38;
            Item.scale = 0.5f;
            Item.maxStack = 1;
            Item.value = 0;
            Item.accessory = true;
            if (ModCompatibility.calamityEnabled)
            {
                Item.rare = ModContent.RarityType<DarkBlue>();
            }
            else
            {
                Item.rare = ItemRarityID.Purple;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient(ItemID.GuideVoodooDoll,1);
            if (ModCompatibility.calamityEnabled) {
                recipe.AddIngredient(ItemID.LunarBar, 20);
                recipe.AddIngredient(CalamityWeakRef.ItemType.UnholyEssence, 20);
                recipe.AddIngredient(CalamityWeakRef.ItemType.GalacticaSingularity, 5);
                recipe.AddIngredient(ItemID.FragmentSolar, 3);
                recipe.AddIngredient(CalamityWeakRef.ItemType.Phantoplasm, 20);
            } else {
                recipe.AddIngredient(ItemID.LunarOre, 30);
                recipe.AddIngredient(ItemID.FragmentSolar, 15);
                recipe.AddIngredient(ItemID.FragmentVortex, 10);
                recipe.AddIngredient(ItemID.FragmentNebula, 15);
                recipe.AddIngredient(ItemID.FragmentStardust, 10);
            }
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool AllowPrefix(int pre)
        {
            return false;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Terrideimo>().canHurtDeimos = true;
        }
    }
}