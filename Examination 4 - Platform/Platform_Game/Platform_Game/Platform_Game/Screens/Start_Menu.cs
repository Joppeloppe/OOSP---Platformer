using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Screens
{
    public class Start_Menu
    {
        Game1 game_1;

        Texture2D tex;

        public Start_Menu(Game1 game_1)
        {
            this.game_1 = game_1;

            tex = game_1.Content.Load<Texture2D>(@"Image/Screen/start_screen_test");
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                game_1.Start_game();
        }

        public void Draw (SpriteBatch sprite_batch)
        {
            sprite_batch.Draw(tex, Vector2.Zero, Color.White);
        }
    }
}
