using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform_Game.Particle_Engine
{
    public class Particle_engine
    {
        private Random random;

        public Vector2 emitter_location;

        private List<Particle> particles;
        private List<Texture2D> textures;

        public Particle_engine(List<Texture2D> textures, Vector2 location)
        {
            emitter_location = location;

            this.textures = textures;
            this.particles = new List<Particle>();

            random = new Random();
        }

        public void Update()
        {
            int total = 1;

            for (int i = 0; i < total; i++)
            {
                particles.Add(Generate_New_Particle());
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();

                if (particles[particle].ttl <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw (SpriteBatch sprite_batch)
        {

            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(sprite_batch);
            }

        }
        private Particle Generate_New_Particle()
        {
            Texture2D tex = textures[random.Next(textures.Count)];
            Vector2 pos = emitter_location;

            Vector2 speed = new Vector2(0.5f * (float)(random.NextDouble() * 2 - 1),
                0.5f * (float)(random.NextDouble() * 2 - 1));

            float angle = 0;
            float angle_speed = 0.1f * (float)(random.NextDouble() * 2 - 1);

            Color color = new Color(255, (float)random.NextDouble(), 0);

            float size = (float)random.NextDouble();
            int ttl = 10 + random.Next(20);

            return new Particle(pos, tex, speed, angle, angle_speed, color, size, ttl);
        }
    }
}
