using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Object
{
    class Game_Object
    {
        public Vector2 pos, prev_pos, start_pos, speed;

        public Texture2D tex;

        protected Rectangle hit_box, prev_hit_box;

        protected bool is_on_ground, is_attacking, powered_player, has_jump_boost;

        protected int elapsed_time;
        protected int time_duration = 2;
        protected int attack_duration = 100;

        public static int score;

        public static List<Power_Up> projectiles = new List<Power_Up>();

        public Game_Object(Vector2 pos, Texture2D tex)
        {
            this.pos = pos;
            this.tex = tex;

            this.hit_box = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

            this.start_pos = pos;

            this.speed = new Vector2(0, 0);
        }

        public virtual void Update(GameTime game_time)
        {
            if (is_on_ground)
                speed.Y = 0;

            pos += speed;

            hit_box.X = (int)(pos.X >= 0 ? pos.X + 0.5f : pos.X - 0.5f);
            hit_box.Y = (int)(pos.Y >= 0 ? pos.Y + 0.5f : pos.Y - 0.5f);
        }

        public virtual void Draw(SpriteBatch sprite_batch)
        {
            foreach (Power_Up p in projectiles)
                sprite_batch.Draw(tex, pos, Color.White);

            sprite_batch.Draw(tex, pos, Color.White);
        }


        public virtual void Timer (GameTime game_time)
        {
            if (is_attacking)
            {
                elapsed_time += (int)game_time.ElapsedGameTime.TotalMilliseconds;

                if (elapsed_time >= time_duration)
                {
                    --attack_duration;

                    elapsed_time -= time_duration;
                }
            }

            if (attack_duration <= 0)
            {
                is_attacking = false;

                attack_duration = 100;
            }
        }

        public virtual bool Hits_object(Game_Object go)
        {
            return hit_box.Intersects(go.hit_box);
        }

        public virtual bool Climb_available(Platform p)
        {
            return (hit_box.Intersects(p.hit_box) && Keyboard.GetState().IsKeyDown(Keys.W));
        }

        public virtual bool Is_shooting()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && powered_player);
        }

        public virtual void Handle_collision(Platform p, Game_Object go)
        {
            Rectangle inter = Rectangle.Intersect(go.hit_box, p.hit_box);
            Vector2 nor_velocity = new Vector2(go.speed.X, go.speed.Y);
            nor_velocity.Normalize();

            if (inter.Height - 4 > inter.Width)
            {
                if (go.pos.X < p.hit_box.X)
                {
                    go.speed.X = 0;
                    go.pos.X -= inter.Width;
                }

                else
                    go.pos.X += inter.Width;
            }

            else
            {
                if (go.pos.Y < p.hit_box.Y)
                {
                    go.speed.Y = 0;
                    go.pos.Y -= inter.Height;
                    go.is_on_ground = true;
                }

                else
                    go.pos.Y += inter.Height;
            }
        }

        public virtual void Handle_enemy_collision(Enemy e)
        {
            if (!is_attacking)
                pos = start_pos;

            if (is_attacking)
            {
                e.is_dead = true;
                Content_Manager.destroy_enemy.Play();
            }
        }

        public virtual void Handle_powered_player()
        {
            powered_player = true;
        }

        public virtual void Handle_point_item()
        {
            score += 10;
            Content_Manager.gets_point.Play();
        }

        public virtual void Handle_jump_boost()
        {
            has_jump_boost = true;
        }

        public virtual void Handle_trampoline(Game_Object go)
        {
            go.speed.Y = -10;
        }

        public virtual void Handle_fall()
        {
            is_on_ground = false;
        }

        public virtual void Handle_shooting(Game_Object go, Texture2D tex)
        {
            projectiles.Add(new Power_Up(go.pos, tex));

            foreach (Power_Up p in projectiles)
                p.speed.X = 7;

            powered_player = false;
        }

        public virtual void Handle_enemy_movement(Enemy e)
        {
            e.speed.X *= -1;
        }

        public virtual void Handle_climbing(Platform p, Game_Object go)
        {
            speed.Y = -2;
        }

        public virtual bool Pixel_Collision(Game_Object go)
        {
            Color[] data_a = new Color[tex.Width * tex.Height];
            tex.GetData(data_a);

            Color[] data_b = new Color[go.tex.Width * go.tex.Height];
            go.tex.GetData(data_b);

            int top = Math.Max(hit_box.Top, go.hit_box.Top);
            int bottom = Math.Min(hit_box.Bottom, go.hit_box.Bottom);
            int left = Math.Max(hit_box.Left, go.hit_box.Left);
            int right = Math.Min(hit_box.Right, go.hit_box.Right);

            for (int y = top; y < bottom; y++)
			{
			    for (int x = left; x < right; x++)
			    {
			        Color color_a = data_a[(x - hit_box.Left) + (y - hit_box.Top) * hit_box.Width];

                    Color color_b = data_b[(x - go.hit_box.Left) + (y - go.hit_box.Top) * go.hit_box.Width];

                    if (color_a.A != 0 && color_b.A != 0)
                    {
                        return true;
                    }
			    }
			}

            return false;
        }
    }
}
