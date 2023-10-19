using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Items;
using CalamityMod;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;

namespace EverquartzAdventure.Items
{
    public class CelestialGeode : ModItem
    {

        public override string Texture => "EverquartzAdventure/Items/Weapons/no";
        
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.FrostCore);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.width = 14;
            Item.height = 28;
            Item.rare = ItemRarityID.Purple;
        }
        /*public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            if (ModCompatibility.calamityEnabled)
            {
                recipe.AddIngredient(CalamityWeakRef.ItemType.ExodiumCluster, 10);
            }
            else
            {
                recipe.AddIngredient(ItemID.LunarOre, 10);
            }
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }*/
    }
}