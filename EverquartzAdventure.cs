using System.Collections.Generic;
using Terraria.ModLoader;
using System.Linq;
using Terraria;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.GameContent.UI;
using EverquartzAdventure.NPCs.TownNPCs;
using EverquartzAdventure.NPCs;
using System;
using Terraria.Localization;
using EverquartzAdventure.Items.Critters;
using Terraria.ModLoader.IO;
using System.Collections;
using EverquartzAdventure.ILEditing;
using EverquartzAdventure.UI.Transmogrification;
using EverquartzAdventure.Items.TransRecipes;
using EverquartzAdventure.Items;

namespace EverquartzAdventure
{
    public class EverquartzAdventureMod : Mod
    {
        public static EverquartzAdventureMod Instance { get; private set; }




        public override void PostSetupContent()
        {
            //ModCompatibility.calamityEnabled = ModLoader.HasMod("CalamityMod");
            //ModLoader.TryGetMod("Census", out Mod censusMod);
            //if (censusMod != null)
            //{
            //    censusMod.Call("TownNPCCondition", ModContent.NPCType<StarbornPrincess>(), "Brutally murder her mom");
            //}
            TryDoCensusSupport();
            TransmogrificationManager.LoadAllTrans();
            //Logger.Info(TransmogrificationManager.Transmogrifications.Count());
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            EverquartzMessageType msgType = (EverquartzMessageType)reader.ReadByte();
            switch (msgType)
            {
                case EverquartzMessageType.DeimosItemKilled:
                    //Main.player[reader.ReadInt32()];
                    Player murderer = Main.player[reader.ReadInt32()];
                    int helptext = reader.ReadInt32();
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        StarbornPrincess.ItemDeathEffectServer(murderer, helptext);
                    }
                    else
                    {

                        StarbornPrincess.ItemDeathEffectClient(murderer.position, murderer.width, murderer.height, helptext);
                    }

                    break;
                case EverquartzMessageType.ReleaseProvCore:
                    Player player = Main.player[reader.ReadInt32()];
                    DivineCore.ReleaseProvCoreServer(player);
                    break;
            }
        }

        //public static int GeodeCurrencyId;

        public override void AddRecipes() //Transmogrification in recipes
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<Items.Placeable.MusicBoxes.MelanieMartinezMusicBox>());
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ModContent.ItemType<Items.DeimosFumo>(), 10);
            recipe.AddIngredient<TimeMinute>(5);
            recipe.AddTile<TransmStation>();
            recipe.DisableDecraft();
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<Items.Weapons.DeimosFumoCannon>());
            recipe.AddIngredient(ItemID.PortalGun, 1);
            recipe.AddIngredient(ModContent.ItemType<Items.DeimosFumo>(), 1);
            recipe.AddIngredient<TimeSecond>(30);
            recipe.AddTile<TransmStation>();
            recipe.DisableDecraft();
            recipe.Register();
            if (ModCompatibility.calamityEnabled)
            {
                recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.ClockGatlignum>());
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.AstralBlaster>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.SeaPrism>(), 10);
                recipe.AddIngredient<TimeSecond>(20);
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.TheMicrowave> ());
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Oracle>(), 1);
                recipe.AddIngredient(ItemID.FallenStar, 40);
                recipe.AddIngredient<TimeSecond>(30);
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ItemID.NorthPole);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.DraedonsArsenal.PoleWarper>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.EndothermicEnergy>(), 5);
                recipe.AddIngredient<TimeMinute>();
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Pets.BloodyVein>());
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Pets.RottingEyeball>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.RottenMatter>(), 22);
                recipe.AddIngredient<TimeMinute>(30);
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ItemID.Shrimp);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.LeviathanAmbergris>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Nucleogenesis>(), 1);
                recipe.AddIngredient<TimeSecond>(20);
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ItemID.WaterBucket);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.CryoStone>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.EssenceofSunlight>(), 1);
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Weapons.Magic.LightGodsBrilliance>());
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.ShardofAntumbra>(), 1);
                recipe.AddIngredient(ItemID.SoulofLight, 5);
                recipe.AddIngredient<TimeMinute>();
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Potions.DeliciousMeat>());
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Accessories.Wings.SoulofCryogen>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.CryonicBar>(), 25);
                recipe.AddIngredient<TimeMinute>(5);
                recipe.AddTile<TransmStation>();
                recipe.DisableDecraft();
                recipe.Register();

                recipe = Recipe.Create(ModContent.ItemType<Items.Mineral>());
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Rock>(), 1);
                recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.ShadowspecBar>(), 5);
                recipe.AddIngredient<TimeSecond>(30);
                recipe.AddTile<TransmStation>();
                recipe.AddCustomShimmerResult(ModContent.ItemType<CalamityMod.Items.Rock>(), 1);
                recipe.Register();

                if (ModCompatibility.calRemixEnabled)
                {
                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.DivineGeode>(), 20);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.UnholyEssence>(), 20);
                    recipe.AddIngredient<TimeMinute>();
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();

                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.RuinousSoul>(), 10);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.Polterplasm>(), 10);
                    recipe.AddIngredient<TimeMinute>();
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();

                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.NightmareFuel>(), 20);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.EndothermicEnergy>(), 20);
                    recipe.AddIngredient<TimeMinute>();
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();

                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.EndothermicEnergy>(), 20);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.NightmareFuel>(), 20);
                    recipe.AddIngredient<TimeMinute>();
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();

                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.DarksunFragment>(), 20);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ItemID.SoulofNight, 20);
                    recipe.AddIngredient<TimeMinute>();
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();

                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.YharonSoulFragment>(), 5);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Placeables.Ores.AuricOre>(), 5);
                    recipe.AddIngredient<TimeMinute>(5);
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();

                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.ExoPrism>(), 5);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.AshesofAnnihilation>(), 5);
                    recipe.AddIngredient<TimeMinute>(10);
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();

                    recipe = Recipe.Create(ModContent.ItemType<CalamityMod.Items.Materials.AshesofAnnihilation>(), 5);
                    recipe.AddIngredient(CalRemixWeakRef.ItemType.YharimBar, 1);
                    recipe.AddIngredient(ModContent.ItemType<CalamityMod.Items.Materials.ExoPrism>(), 5);
                    recipe.AddIngredient<TimeMinute>(10);
                    recipe.AddTile<TransmStation>();
                    recipe.DisableDecraft();
                    recipe.Register();
                }
            }
        }

        public override void Load()
        {
            base.Load();
            Instance = this;

            //GeodeCurrencyId = CustomCurrencyManager.RegisterCurrency(new NPCs.TownNPCs.GeodeCurrency(ModContent.ItemType<Items.CelestialGeode>(), 1L, "Mods.EverquartzAdventure.Currencies.GeoCurTextKey"));
            ModCompatibility.censusMod = null;
            ModLoader.TryGetMod("Census", out ModCompatibility.censusMod);
            ModCompatibility.hypnosMod = null;
            ModLoader.TryGetMod("HypnosMod", out ModCompatibility.hypnosMod);

            ModCompatibility.calamityEnabled = ModLoader.HasMod("CalamityMod");
            ModCompatibility.hypnosEnabled = ModLoader.HasMod("HypnosMod");
            ModCompatibility.calRemixEnabled = ModLoader.HasMod("CalRemix");

            ILChanges.Load();

            
            //if (ModCompatibility.calamityEnabled)
            //{
            //    CalamityILChanges.Load();
            //}


        }



        public override void Unload()
        {
            base.Unload();

            //if (ModCompatibility.calamityEnabled)
            //{
            //    CalamityILChanges.Unload();
            //}
            TransmogrificationManager.UnloadAllTrans();

            ILChanges.Unload();

            ModCompatibility.calamityEnabled = false;
            ModCompatibility.hypnosEnabled = false;
            ModCompatibility.calRemixEnabled = false;

            ModCompatibility.censusMod = null;
            ModCompatibility.hypnosMod = null;

            

            Instance = null;
            
        }

        public override object Call(params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return null;
            }
            if (!(args[0] is string argStr))
            {
                return null;
            }
            switch (argStr)
            {
                case "Transmogrification":
                case "AddTransmogrification":
                case "AddTrans":
                case "RegisterTransmogrification":
                case "RegisterTrans":
                    TransmogrificationManager.AddFromModCall(args.Skip(1));
                    return null;
                default:
                    return null;
            }
        }

        private void TryDoCensusSupport()
        {
            Mod censusMod = ModCompatibility.censusMod;
            if (censusMod != null)
            {
                censusMod.Call("TownNPCCondition", ModContent.NPCType<StarbornPrincess>(), Language.GetTextValue(StarbornPrincess.CensusConditionKey));
            }
        }

    }


    public class EverquartzSystem : ModSystem
    {



        public static List<int> UniqueNPCs => new List<int>() {
            ModContent.NPCType<StarbornPrincess>(),
        };

        public override void PreUpdateNPCs()
        {
            UniqueNPCs.ForEach(AntiDupe);
        }



        public static void AntiDupe(int type)
        {
            IEnumerable<NPC> possiblyMultipleDeimi = Main.npc.Where(npc => npc != null && npc.active && npc.type == type);
            if (possiblyMultipleDeimi.Count() > 1)
            {
                possiblyMultipleDeimi.SkipLast(1).ToList().ForEach(npc => { npc.netUpdate = true; npc.active = false; }) ;
            }
        }
    }




    public enum EverquartzMessageType
    {
        DeimosItemKilled, // id, player.whoAmI, helptext
        ReleaseProvCore, // id, player.whoAmI
        HypnosReward, // id, player.whoAmI, rewards(bytes)
        HypnoCoinAdd, // id
        HypnosDeparted, // id
        //EverquartzSyncPlayer // id, player.whoAmI (see EverquartzPlayer.SyncPlayer)
    }

    public static class ModCompatibility
    {
        public static bool calamityEnabled = false;
        public static bool hypnosEnabled = false;
        public static bool calRemixEnabled = false;
        public static Mod censusMod;
        public static Mod hypnosMod;
        private static int? hypnosBossType = null;
        public static int? HypnosBossType
        {
            get
            {
                if (!hypnosBossType.HasValue)
                {
                    ModNPC hyNPC = null;
                    hypnosMod?.TryFind<ModNPC>("HypnosBoss", out hyNPC);
                    hypnosBossType = hyNPC?.Type;

                }
                return hypnosBossType;
            }
            set
            {
                hypnosBossType = value;
            }
        }
    }

    [JITWhenModsEnabled("CalamityMod")]
    internal static partial class CalamityWeakRef
    {

    }

    [JITWhenModsEnabled("Hypnos")]
    internal static partial class HypnosWeakRef
    {

    }

    [JITWhenModsEnabled("CalRemix")]
    internal static partial class CalRemixWeakRef
    {

    }

    
    
}