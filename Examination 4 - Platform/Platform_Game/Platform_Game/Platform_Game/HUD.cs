using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platform_Game.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game
{
    class HUD
    {
        Game1 game_1;

        Vector2 pos;

        SpriteFont font;

        public HUD (Game1 game_1, SpriteFont font)
        {
            this.game_1 = game_1;
            this.font = font;

            pos = new Vector2(100, 0);
        }

        public void Update()
        {
        }

        public void Draw (SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(font, "Your current score is " + Game_Object.score, pos, Color.White);
        }

}
}
