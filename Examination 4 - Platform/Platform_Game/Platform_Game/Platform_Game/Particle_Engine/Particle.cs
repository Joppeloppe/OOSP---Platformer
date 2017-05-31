using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Particle_Engine
{
    public class Particle
    {
        public Vector2 pos, speed, origin;

        public Texture2D tex;

        public float angle, angle_speed, size;

        public int ttl;

        public Color color;

        Rectangle source_rec;

        public Particle (Vector2 pos, Texture2D tex, Vector2 speed, float angle, float angle_speed, Color color, float size, int ttl)
        {
            this.pos = pos;
            this.tex = tex;
            this.speed = speed;
            this.angle = angle;
            this.angle_speed = angle_speed;
            this.color = color;
            this.size = size;
            this.ttl = ttl;
        }

        public void Update()
        {
            ttl--;
            pos += speed;
            angle += angle_speed;
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            source_rec = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);

            sprite_batch.Draw(tex, pos, source_rec, color, angle, origin, size, SpriteEffects.None, 0);
        }

    }

}
