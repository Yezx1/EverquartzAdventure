using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EverquartzAdventure.Items.TransRecipes;

namespace EverquartzAdventure.Items
{
    public class TransmStationItem : ModItem
    {

        public override string Texture => "EverquartzAdventure/NPCs/TownNPCs/StarbornPrincess_Head";
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 42;
            Item.maxStack = 24;
            Item.value = 0;
            Item.rare = ItemRarityID.Pink;
            base.Item.consumable = true;
            base.Item.createTile = ModContent.TileType<TransmStation>();
            base.Item.useAnimation = 20;
            base.Item.useTime = 20;
            base.Item.noUseGraphic = true;
            base.Item.noMelee = true;
            base.Item.rare = ModContent.RarityType<CelestialRarity>();
            base.Item.UseSound = SoundID.Item1;
            base.Item.useStyle = 1;
        }
    }
}