﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace MineBlock.Blocks
{
    class BedRock : Block
    {
         public BedRock(int XPos , int yPos)
        {
            x = XPos;
            y = yPos;
             index = 6;
             canMine = false;
        }
         public override Block Reset(int X, int Y)
         {
             return new BedRock(X, Y);
         }
         public override Block Place(int x, int y)
         {

             return new BedRock(x,y);
         }

         public override Block Mine(int x, int y)
         {

             return new BedRock(x,y);
         }
    }
}
