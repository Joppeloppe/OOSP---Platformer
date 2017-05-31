using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Object
{
    class Enemy : Game_Object
    {

        public bool is_dead;
        public Enemy (Vector2 pos, Texture2D tex) : base (pos, tex)
        {
            this.pos = pos;
            this.tex = tex;

            speed.X = 1.5f;
        }

        public override void Update(GameTime game_time)
        {
            prev_pos = pos;
            prev_hit_box = new Rectangle((int)prev_pos.X, (int)prev_pos.Y, tex.Width, tex.Height);

            if (!is_on_ground)
                speed.Y += 0.2f;

                //hahaha

            base.Update(game_time);
        }
    }
}
