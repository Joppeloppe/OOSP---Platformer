using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platform_Game.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Screens
{
    class Current_Level : Content_Manager
    {
        Game1 game_1;

        public Current_Level(Game1 game_1) : base (game_1)
        {
            this.game_1 = game_1;

            if (game_1.level_numb == 1)
                Load_Level("level_1.txt");

            if (game_1.level_numb == 2)
                Load_Level("level_2.txt");
        }

        public void Update(GameTime game_time)
        {
            Update_level(game_time);
        }

        public void Draw (SpriteBatch sprite_batch)
        {
            Draw_level(sprite_batch);
        }

    }
}
