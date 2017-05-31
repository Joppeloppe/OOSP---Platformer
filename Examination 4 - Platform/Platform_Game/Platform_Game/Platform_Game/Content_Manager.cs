using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Platform_Game.Object;
using Platform_Game.Particle_Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform_Game
{
    abstract class Content_Manager
    {
        Game1 game_1;
        Player player;
        Background background;
        HUD hud;
        Particle_engine particle_engine;

        Texture2D player_tex, enemy_tex, platform_tex, goal_tex, power_up_tex, bg_tex, point_item_tex, jump_boost_tex, trampoline_tex, ladder_tex;

        SpriteFont hud_font;
        public static SoundEffect gets_point, destroy_enemy;

        List<String> level_objects = new List<String>();
        List<Texture2D> particle_textures = new List<Texture2D>();

        List<Platform> platforms = new List<Platform>();
        List<Platform> stoppers = new List<Platform>();
        List<Enemy> enemies = new List<Enemy>();
        List<Goal> goals = new List<Goal>();
        List<Power_Up> power_ups = new List<Power_Up>();
        List<Power_Up> jump_boosts = new List<Power_Up>();
        List<Power_Up> trampolines = new List<Power_Up>();
        List<Point_item> point_items = new List<Point_item>();
        List<Platform> ladders = new List<Platform>();

        public Content_Manager(Game1 game_1)
        {
            this.game_1 = game_1;

            player_tex = game_1.Content.Load<Texture2D>(@"Image/Character/robot");
            enemy_tex = game_1.Content.Load<Texture2D>(@"Image/Character/chomper_robot");

            platform_tex = game_1.Content.Load<Texture2D>(@"Image/Tile/tile_test");

            goal_tex = game_1.Content.Load<Texture2D>(@"Image/Item/flag");

            power_up_tex = game_1.Content.Load<Texture2D>(@"Image/Item/robot_power_glove");
            point_item_tex = game_1.Content.Load<Texture2D>(@"Image/Item/pink_battery");
            jump_boost_tex = game_1.Content.Load<Texture2D>(@"Image/Item/rocket_boots");
            trampoline_tex = game_1.Content.Load<Texture2D>(@"Image/Item/trampoline");
            ladder_tex = game_1.Content.Load<Texture2D>(@"Image/Item/ladder");

            particle_textures.Add(game_1.Content.Load<Texture2D>(@"Image/Particle/circle"));

            hud_font = game_1.Content.Load<SpriteFont>(@"Font/hud_font");

            gets_point = game_1.Content.Load<SoundEffect>(@"Sound/eat_candy");
            destroy_enemy = game_1.Content.Load<SoundEffect>(@"Sound/hit");

            particle_engine = new Particle_engine(particle_textures, Vector2.Zero);
        }

        public void Load_Level(String level_name)
        {
            StreamReader level_reader = new StreamReader(level_name);

            while (!level_reader.EndOfStream)
                level_objects.Add(level_reader.ReadLine());

            level_reader.Close();

            for (int i = 0; i < level_objects.Count; i++)
            {
                for (int j = 0; j < level_objects[i].Length; j++)
                {
                    if (level_objects[i][j] == 'x')
                        platforms.Add(new Platform(new Vector2(j * 50, i * 50), platform_tex));

                    if (level_objects[i][j] == 'y')
                        stoppers.Add(new Platform(new Vector2(j * 50, i * 50), platform_tex));

                    if (level_objects[i][j] == 'p')
                        player = new Player(new Vector2(j * 50, i * 50), player_tex, game_1.Window);

                    if (level_objects[i][j] == 'e')
                        enemies.Add(new Enemy(new Vector2(j * 50, i * 50), enemy_tex));

                    if (level_objects[i][j] == 'g')
                        goals.Add(new Goal(new Vector2(j * 50, i * 50), goal_tex));

                    if (level_objects[i][j] == 's')
                        power_ups.Add(new Power_Up(new Vector2(j * 50, i * 50), power_up_tex));

                    if (level_objects[i][j] == 'j')
                        jump_boosts.Add(new Power_Up(new Vector2(j * 50, i * 50), jump_boost_tex));

                    if (level_objects[i][j] == 't')
                        trampolines.Add(new Power_Up(new Vector2(j * 50, i * 50), trampoline_tex));

                    if (level_objects[i][j] == 'i')
                        point_items.Add(new Point_item(new Vector2(j * 50, i * 50), point_item_tex));

                    if (level_objects[i][j] == 'l')
                        ladders.Add(new Platform(new Vector2(j * 50, i * 50), ladder_tex));
                }
            }

            background = new Background(bg_tex, game_1);

            hud = new HUD(game_1, hud_font);
        }

        public void Update_level(GameTime game_time)
        {
            player.Update(game_time);

            if (player.Is_shooting())
                player.Handle_shooting(player, power_up_tex);


            foreach (Platform p in platforms)
            {
                p.Update(game_time);

                if (player.Hits_object(p))
                {
                    player.Handle_collision(p, player);
                    break;
                }

                else
                    player.Handle_fall();

                foreach (Enemy e in enemies)
                    if (e.Hits_object(p))
                        e.Handle_collision(p, e);
            }

            foreach (Platform s in stoppers)
            {
                s.Update(game_time);

                foreach (Enemy e in enemies)
                    if (e.Hits_object(s))
                        e.Handle_enemy_movement(e);
            }

            foreach (Enemy e in enemies)
            {
                e.Update(game_time);

                if (player.Hits_object(e))
                    player.Handle_enemy_collision(e);

                if (e.is_dead)
                {
                    enemies.Remove(e);
                    break;
                }

            }

            foreach (Goal g in goals)
            {
                g.Update(game_time);

                if(player.Hits_object(g))
                {
                    if (player.Pixel_Collision(g))
                        game_1.Won_Level();
                }
            }

            foreach (Power_Up p in power_ups)
            {
                p.Update(game_time);

                if (player.Hits_object(p))
                {
                    player.Handle_powered_player();

                    power_ups.Remove(p);
                    break;
                }

            }

            foreach (Power_Up p in Game_Object.projectiles)
            {
                p.Update(game_time);

                particle_engine.emitter_location = new Vector2(p.pos.X, p.pos.Y + p.tex.Height / 2);
                particle_engine.Update();  

                foreach (Enemy e in enemies)
                {
                    if (p.Hits_object(e))
                    {
                        enemies.Remove(e);
                        Game_Object.projectiles.Remove(p);
                        return;
                    }
                }

            }

            foreach (Power_Up j in jump_boosts)
            {
                j.Update(game_time);

                if (player.Hits_object(j))
                {
                    player.Handle_jump_boost();

                    jump_boosts.Remove(j);
                    break;
                }

                particle_engine.emitter_location = new Vector2(j.pos.X, j.pos.Y + j.tex.Height);
                particle_engine.Update();  

            }

            foreach (Power_Up t in trampolines)
            {
                t.Update(game_time);

                if (player.Hits_object(t))
                    player.Handle_trampoline(player);
            }

            foreach (Point_item p in point_items)
            {
                p.Update(game_time);

                if (player.Hits_object(p))
                {
                    player.Handle_point_item();

                    point_items.Remove(p);
                    break;
                }
            }

            foreach (Platform l in ladders)
            {
                l.Update(game_time);

                if (player.Climb_available(l))
                    player.Handle_climbing(l, player);
            }

            background.Update();

        }

        public void Draw_level(SpriteBatch sprite_batch)
        {
            background.Draw(sprite_batch);

            hud.Draw(sprite_batch);

            player.Draw(sprite_batch);

            foreach (Goal g in goals)
                g.Draw(sprite_batch);

            foreach (Platform p in platforms)
                p.Draw(sprite_batch);

            foreach (Enemy e in enemies)
                e.Draw(sprite_batch);

            foreach (Power_Up p in power_ups)
                p.Draw(sprite_batch);

            foreach (Power_Up j in jump_boosts)
                j.Draw(sprite_batch);

            foreach (Power_Up t in trampolines)
                t.Draw(sprite_batch);

            foreach (Power_Up p in Game_Object.projectiles)
                p.Draw(sprite_batch);

            foreach (Point_item p in point_items)
                p.Draw(sprite_batch);

            foreach (Platform l in ladders)
                l.Draw(sprite_batch);

            particle_engine.Draw(sprite_batch);

        }

        public void Clear_level()
        {
            goals.Clear();
            platforms.Clear();
            enemies.Clear();
            power_ups.Clear();
            point_items.Clear();
        }
    }
}
