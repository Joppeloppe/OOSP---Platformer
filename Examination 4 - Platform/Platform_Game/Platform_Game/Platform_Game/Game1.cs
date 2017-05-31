using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Platform_Game.Screens;

namespace Platform_Game
{
    enum Screen
    {
        Start_Menu,
        Game_Win,
        Game_Over,
        Current_Level

    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sprite_batch;

        public Start_Menu start_menu;
        public Game_Win game_win;
        public Game_Over game_over;
        Current_Level current_level;

        Screen current_screen;

        public int level_numb;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;

            Window.Title = "Platformer game - Robot";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sprite_batch = new SpriteBatch(GraphicsDevice);

            start_menu = new Start_Menu(this);
            current_screen = Screen.Start_Menu;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            switch (current_screen)
            {
                case Screen.Start_Menu:
                    start_menu.Update();
                    break;

                case Screen.Game_Over:
                    game_over.Update();
                    break;

                case Screen.Game_Win:
                    game_win.Update();
                    break;

                case Screen.Current_Level:
                    current_level.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sprite_batch.Begin();

            switch (current_screen)
            {
                case Screen.Start_Menu:
                    start_menu.Draw(sprite_batch);
                    break;

                case Screen.Game_Over:
                    game_over.Draw(sprite_batch);
                    break;

                case Screen.Game_Win:
                    game_win.Draw(sprite_batch);
                    break;

                case Screen.Current_Level:
                    current_level.Draw(sprite_batch);
                    break;
            }

            sprite_batch.End();

            base.Draw(gameTime);
        }

        public void Start_game()
        {
            ++level_numb;

            current_level = new Current_Level(this);
            current_screen = Screen.Current_Level;
        }

        public void Won_Level()
        {
            game_win = new Game_Win(this);
            current_screen = Screen.Game_Win;
        }

        public void Lost_Game()
        {
            game_over = new Game_Over(this);
            current_screen = Screen.Game_Over;
        }

    }
}
