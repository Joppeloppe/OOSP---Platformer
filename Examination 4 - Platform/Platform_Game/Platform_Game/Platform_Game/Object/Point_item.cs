using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Object
{
    class Point_item : Game_Object
    {
        public Point_item (Vector2 pos, Texture2D tex) : base (pos, tex)
        {
            this.pos = pos;
            this.tex = tex;
        }
    }
}
