using Terraria;
using EverquartzAdventure.Items;
using Terraria.ID;
using Terraria.ModLoader;
using EverquartzAdventure.Tiles;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using EverquartzAdventure.Items.Weapons;

namespace EverquartzAdventure.Projectiles
{
    public class DeimosFumoThrown : ModProjectile
    {
        public override void SetDefaults()
        {
            AIType = -1;
            Projectile.aiStyle = -1; //10
            Projectile.friendly = false;
            Projectile.hostile = false;
            //Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 600;
            DrawOriginOffsetY = -12;
            DrawOffsetX = -4;

        }

        bool dropItem = true;
        public bool wasShot = false;

        /*public override void OnSpawn(IEntitySource source)
        { //only when its shot from the deimos fumo launcher
            if (source is EntitySource_Parent parent && parent.Entity is Item shotfrom && shotfrom.type == ModContent.ItemType<DeimosFumoCannon>())
            {
                wasShot = true;
                timeFromShot = 0;
            }
        }*/
        public override void AI() //vanilla gravestone projectile ai
        {
            Point deiPos = Projectile.position.ToTileCoordinates();
            if (Projectile.shimmerWet)
            {
                if (Projectile.velocity.Y > 10f)
                {
                    Projectile.velocity.Y *= 0.97f;
                }
                Projectile.velocity.Y -= 0.7f;
                if (Projectile.velocity.Y < -10f)
                {
                    Projectile.velocity.Y = -10f;
                }
            }
            if (Projectile.velocity.Y == 0f)
            {
                Projectile.velocity.X *= 0.98f;
            }
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.velocity.Y += 0.2f;
            int deiPoX1 = (int)((Projectile.position.X + (Projectile.width / 2)) / 16f);
            int deiPoY1 = (int)((Projectile.position.Y + Projectile.height - 4f) / 16f);
            if (Main.tile[deiPoX1, deiPoY1] == null)
            {

                //WorldGen.PlaceTile(deiPos.X, deiPos.Y, ModContent.TileType<DeimosFumoPlaced>());
                //SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                //Projectile.Kill();
                return;
            }
            int style = 0;
            bool placeable = false;
            TileObject objectData = default(TileObject);
            if (TileObject.CanPlace(deiPoX1, deiPoY1, ModContent.TileType<DeimosFumoPlaced>(), style, Projectile.direction, out objectData))
            {
                placeable = TileObject.Place(objectData);
            }
            if (placeable)
            {
                NetMessage.SendObjectPlacement(-1, deiPoX1, deiPoY1, ModContent.TileType<DeimosFumoPlaced>(), objectData.style, objectData.alternate, objectData.random, Projectile.direction);
                //Point deiPos = Projectile.position.ToTileCoordinates();
                //WorldGen.PlaceTile(deiPos.X, deiPos.Y, ModContent.TileType<DeimosFumoPlaced>());
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                dropItem = false;
                Projectile.Kill();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //int deiPoX1 = (int)(Projectile.position.X + ((Projectile.width / 2) / 16f));
            //int deiPoY1 = (int)(Projectile.position.Y + ((Projectile.height / 2) / 16f));
            /*int deiPoX1 = (int)(Projectile.position.X + 24);
            int deiPoY1 = (int)(Projectile.position.Y + 24);
            if (Main.tile[deiPoX1, deiPoY1] == null)
            {

                //WorldGen.PlaceTile(deiPos.X, deiPos.Y, ModContent.TileType<DeimosFumoPlaced>());
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                Projectile.Kill();
            }*/
            /*Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {*/
            //Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height); 
            if (oldVelocity.Y >= 8.5f || oldVelocity.X >= 9.5f)
            {
                if (Main.rand.NextBool(2))
                {
                    SoundEngine.PlaySound(SoundID.Item57, Projectile.position);
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.Item58, Projectile.position);
                }
                for (int i = 0; i < 15; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, 0, Color.Lavender, 2f);
                }
            }
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = oldVelocity.X * -0.75f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1.5d)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.7f;
            }
            //}
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            int Droping = 0;
            SoundEngine.PlaySound(SoundID.LucyTheAxeTalk, Projectile.position);
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, (Projectile.velocity.X * 0.25f), (Projectile.velocity.Y * 0.25f), 64, Color.Lavender, 1.5f);
            }
            if (dropItem)
            {
                Droping = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ModContent.ItemType<DeimosFumo>());
                if (Main.netMode == NetmodeID.MultiplayerClient && Droping >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, Droping, 1f);
                }
            }
        }
    }
}