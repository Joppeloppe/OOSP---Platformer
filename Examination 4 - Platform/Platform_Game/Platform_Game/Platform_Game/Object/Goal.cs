using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Object
{
    class Goal : Game_Object
    {
        public Goal(Vector2 pos, Texture2D tex) : base (pos, tex)
        {
            this.pos = pos;
            this.tex = tex;
        }
    }
}
