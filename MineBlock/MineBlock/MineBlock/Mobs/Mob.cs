﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MineBlock.Mobs;
namespace MineBlock
{
    public class Mob
    {

        //public Sprite sprite;
        public int CurrentChunk;
        bool isWalking = false;
        protected int Dir = 0; // 1 = left 2 = right
        public Vector2 subPixel = new Vector2(0, 0);
        public Vector2 Position = new Vector2(0, 0);
        Vector2 Velocity = new Vector2(0, 0);
        public bool flip = false;
        readonly Vector2 gravity = new Vector2(0, 259.8f);
        //Block[,] blocks;
        public int name = 0;
        public int Health = 100;

        Rectangle Bar = new Rectangle(0, 0, 62, 20);
        Rectangle Bar2;
        Texture2D HealthBar, Blank;
        SpriteFont pericles1;
        public Mob()
        {
            HealthBar = Tm.getTexture(Tm.Textures.HealthBar);
            Blank = Tm.getTexture(Tm.Textures.Blank);
            pericles1 = Tm.getFont(Tm.Font.f1);
          
        }
        public Mob returnMob(int index, int X, int Y, int chunk)
        {

            switch (index)
            {

                case 1: return new Chicken(X, Y, chunk);
                case 2: return new Cow(X, Y, chunk);
                case 3: return new Pig(X, Y, chunk);
            }
            return new Mob();
        }
        public Block blocks(int x, int y){
            return Chunk.getBlockAt(Game1.Loadedchunks, x, y);
    }
        public virtual void update(GameTime time)
        {

            float elapsed = (float)time.ElapsedGameTime.TotalSeconds;
            subPixel += (Velocity * elapsed);
            //blocks = Game1.chunk;
            blocks((int)Position.X, (int)Position.Y + 1).EntityStandingEvent(this);
            if (subPixel.X > 39)
            {
                subPixel = new Vector2(0, subPixel.Y);
                Position.X++;
            }
            if (subPixel.Y > 39)
            {
                subPixel = new Vector2(subPixel.X, 0);
                Position.Y++;
            }
            if (subPixel.X < 0)
            {
                subPixel = new Vector2(39, subPixel.Y);
                Position.X--;
            }
            if (subPixel.Y < 0)
            {
                subPixel = new Vector2(subPixel.X, 39);
                Position.Y--;
            }

            //sprite.Location = Position;  
            try
            {
                if (blocks(getX(), getY()).index != 0)
                    Velocity = new Vector2(Velocity.X, -15f);
            }
            catch (System.IndexOutOfRangeException) { Console.WriteLine("Mob dun messud up"); }
            
            if (blocks(getX(), getY() + 1).index == 0)
            {

                Velocity += gravity * elapsed;

                //subPixel = new Vector2(subPixel.X, subPixel.Y+9.8f);
            }
            else Velocity = new Vector2(Velocity.X, 0);
            if (!isWalking)
            {
                int random = Game1.randy.Next(0, 100);
                if (random == 3) { Dir = 1; isWalking = true; }
                if (random == 2) { Dir = 2; isWalking = true; }
            }
            else
            {
                int random = Game1.randy.Next(0, 1000);
                if (random == 5)
                {
                    if (Dir == 1) Dir = 2;
                    else if (Dir == 2) Dir = 1;
                }
                if (random < 50) Dir = 0;
                if (Dir == 0) // stop
                {
                    Velocity = new Vector2(0, Velocity.Y);
                    isWalking = false;
                }
                if (Dir == 1) // left
                {
                    try
                    {
                        if (blocks(getX() - 1, getY()).index == 0)
                        {
                            flip = true;
                            Velocity = new Vector2(-75, Velocity.Y);
                        }
                        else if (blocks(getX(), getY() - 1).index == 0 && blocks(getX() - 1, getY() - 1).index == 0) Velocity = new Vector2(-75, -175);
                        else Velocity = new Vector2(0, Velocity.Y);
                    }
                    catch (System.IndexOutOfRangeException) { Dir = 2; }
                }
                if (Dir == 2)// right
                {
                    try
                    {
                        if (blocks(getX() + 1, getY()).index == 0)
                        {
                            flip = false;
                            Velocity = new Vector2(75, Velocity.Y);
                        }
                        else if (blocks(getX(), getY() - 1).index == 0 && blocks(getX() - 1, getY() + 1).index == 0) Velocity = new Vector2(75, -175);
                        else Velocity = new Vector2(0, Velocity.Y);
                    }
                    catch (System.IndexOutOfRangeException) { Dir = 1; }
                }
            }
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (Health < 100)
            {
                Bar = new Rectangle((int)(((Position.X * Constants.BlockSize) + subPixel.X)) - 30, (int)((Position.Y * Constants.BlockSize) + subPixel.Y) - 10, 60, 10);
                Bar2 = new Rectangle(Bar.X + 5, Bar.Y + 2, Health / 2, 4);
                batch.Draw(HealthBar, Bar, Color.White);
                batch.Draw(Blank, Bar2, Health > 50 ? Color.Green : Health > 25 ? Color.Orange : Color.Red);
                batch.DrawString(pericles1, "" + Health, new Vector2(Bar2.X + 16, Bar2.Y - 20), Color.White);
            }
        }
        public void Jump()
        {
            Dir = -2;
            Velocity = new Vector2(0, -175);
            subPixel = new Vector2(subPixel.X, subPixel.Y - 10);
        }
        public int getY()
        {
            return (int)Position.Y;
        }
        public int getX()
        {
            return (int)Position.X;
        }
    }
}
