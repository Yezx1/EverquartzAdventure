using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using EverquartzAdventure.Tiles;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using EverquartzAdventure.Projectiles;

namespace EverquartzAdventure.Items
{
	public class DeimosFumo : ModItem
	{
		public override void SetStaticDefaults() {
            
            // DisplayName.SetDefault("Deimos Plushie");
            //DisplayName.AddTranslation(7, "戴莫斯玩偶");
            //DisplayName.AddTranslation(6, "Плюшевая Игрушка Деймоса");
            //// Tooltip.SetDefault("I hope you don't make it into anything suspiscious...");
            //Tooltip.AddTranslation(7, "我希望你不要对它干奇怪的事情...");
            //Tooltip.AddTranslation(6, "Понадеемся, что ты не будешь делать ничего подозрительного из этой вещи...");
        }
		public override void SetDefaults() {
            //Item.SetFoodDefault();
            Item.width = 40; 
			Item.height = 64; 
			Item.maxStack = Item.CommonMaxStack; 
			Item.value = Item.buyPrice(0, 0, 15, 0);
            Item.rare = ModContent.RarityType<CelestialRarity>();
            Item.ammo = Item.type;
            //Item.shoot = ModContent.ProjectileType<DeimosFumoThrown>();
            base.Item.useStyle = 1;
            base.Item.consumable = true;
            base.Item.createTile = ModContent.TileType<DeimosFumoPlaced>();
            base.Item.useAnimation = 20;
            base.Item.useTime = 20;
            base.Item.autoReuse = true;
            base.Item.noUseGraphic = true;
            base.Item.noMelee = true;
            base.Item.UseSound = SoundID.Item1;
            //base.Item.shoot = ModContent.ProjectileType<DeimosFumoThrown>();
            base.Item.shootSpeed = 10f;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                base.Item.createTile = -1;
                base.Item.shoot = ModContent.ProjectileType<DeimosFumoThrown>();
            }
            else
            {
                base.Item.createTile = ModContent.TileType<DeimosFumoPlaced>();
                base.Item.shoot = ProjectileID.None;
            }
            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        /*public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient<DeimosFumoThrowable>(1);
            recipe.Register();
        }*/
    }
    /*public class DeimosFumoThrowable : ModItem //no longer needed
    {
        public override string Texture => "EverquartzAdventure/Projectiles/DeimosFumoThrown";
        public override void SetDefaults()
        {
            //Item.SetFoodDefault();
            Item.width = 40;
            Item.height = 64;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = ModContent.RarityType<CelestialRarity>();
            base.Item.useStyle = ItemUseStyleID.Swing;
            base.Item.consumable = true;
            base.Item.useAnimation = 20;
            base.Item.shoot = ModContent.ProjectileType<DeimosFumoThrown>();
            base.Item.shootSpeed = 7.5f;
            base.Item.useTime = 20;
            base.Item.autoReuse = true;
            base.Item.noUseGraphic = true;
            base.Item.noMelee = true;
            base.Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(Item.type);
            recipe.AddIngredient<DeimosFumo>(1);
            recipe.Register();
        }
    }*/
}