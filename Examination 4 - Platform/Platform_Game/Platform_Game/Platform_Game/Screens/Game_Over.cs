using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Screens
{
    public class Game_Over
    {
        Game1 game_1;

        Texture2D tex;

        public Game_Over (Game1 game_1)
        {
            this.game_1 = game_1;

            tex = game_1.Content.Load<Texture2D>(@"Image/Screen/game_over_test");
        }

        public void Update()
        {

        }

        public void Draw (SpriteBatch sprite_batch)
        {
            sprite_batch.Draw(tex, Vector2.Zero, Color.White);
        }

    }
}
