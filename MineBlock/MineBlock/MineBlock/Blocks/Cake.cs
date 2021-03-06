﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class Cake : Block
    {
        public Cake(int XPos, int yPos)
        {
            x = XPos;
            y = yPos;
            index = 122;
        }

        public override Block Reset(int X, int Y)
        {

            return new Cake(X, Y);
        }
        public override Block Place(int x, int y)
        {

            return new Cake(x, y);
        }

        public override Block Mine(int x, int y)
        {
            Console.WriteLine("THE CAKE IS A LIE!");
            SoundEffects.RickRoll.Play();
            return new Dirt(x, y);
        }
    }
}
