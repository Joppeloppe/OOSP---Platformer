using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Object
{
    class Player : Game_Object
    {
        GameWindow window;

        int bounds_X, bounds_Y;

        public Player(Vector2 pos, Texture2D tex, GameWindow window)
            : base(pos, tex)
        {
            this.pos = pos;
            this.tex = tex;
            this.window = window;

            this.bounds_X = window.ClientBounds.Width;
            this.bounds_Y = window.ClientBounds.Height;
        }

        public override void Update(GameTime game_time)
        {
            prev_pos = pos;
            prev_hit_box = new Rectangle((int)prev_pos.X, (int)prev_pos.Y, tex.Width, tex.Height);

            speed.X = 0;

            Player_input();

            if (!is_on_ground)
                speed.Y += 0.2f;

            if (powered_player && is_attacking)
                Timer(game_time);

            base.Update(game_time);
        }

        public void Player_input()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) && (pos.X + (tex.Width / 2.0f) > 0))
                speed.X = 2;

            if (Keyboard.GetState().IsKeyDown(Keys.A) && (pos.X - (tex.Width / 2.0f) > 0))
                speed.X = -2;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && is_on_ground)
            {
                speed.Y = -7;
                is_on_ground = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X) && has_jump_boost && is_on_ground)
            {
                speed.Y = -10;
                is_on_ground = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && powered_player)
                is_attacking = true;

            else if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) && !powered_player)
                is_attacking = false;

        }
    }
}
