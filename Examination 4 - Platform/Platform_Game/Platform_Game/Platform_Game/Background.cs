using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game
{
    class Background
    {
        Game1 game_1;

        Texture2D bg_tex;
        Texture2D[] tex_array;

        int bg_spacing;
        float bg_speed;

        List<Vector2> background;

        public Background (Texture2D tex, Game1 game_1)
        {
            //this.bg_tex = game_1.Content.Load<Texture2D>(@"Image/Background/background_test");
            this.game_1 = game_1;

            this.tex_array = new Texture2D[1];

            tex_array[0] = game_1.Content.Load<Texture2D>(@"Image/Background/mountains");

            background = new List<Vector2>();
            bg_spacing = tex_array[0].Width;
            bg_speed = 0.25f;

            for (int i = 0; i < (game_1.Window.ClientBounds.Height /bg_spacing) + 2; i++)
            {
                background.Add(new Vector2(i * bg_spacing, game_1.Window.ClientBounds.Height - tex_array[0].Height));
            }
        }

        public void Update()
        {
            for (int i = 0; i < background.Count; i++)
            {
                background[i] = new Vector2(background[i].X - bg_speed, background[i].Y);

                if (background[i].X <= - bg_spacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = background.Count - 1;
                    }

                    background[i] = new Vector2(background[j].X + bg_spacing - 1, background[j].Y);
                }
            }
        }

        public void Draw (SpriteBatch sprite_batch)
        {
            foreach (Vector2 v in background)
                sprite_batch.Draw(tex_array[0], v, Color.White);
        }
    }
}
