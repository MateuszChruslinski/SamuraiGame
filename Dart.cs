using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SamuraiGame
{
    class Dart
    {
        public static List<Dart> dart = new List<Dart>();

        private Vector2 position = new Vector2(600, 300);
        private int speed = 400;
        private Dir direction;
        public int radious = 32;
        public bool collided = false;

        // Gettery 
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        public Dir Direction
        {
            get
            {
                return direction;
            }
        }

        // Creating new object 
        public Dart(Vector2 newPos, Dir newDir)
        {
            position = newPos;
            direction = newDir;


        }

        // Current location of darts
        public void dartUpdate(GameTime gameTime, bool ingame)
        {
            if (ingame == true)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                switch (direction)
                {
                    case Dir.Down:
                        position.Y += speed * dt;
                        break;
                    case Dir.Up:
                        position.Y -= speed * dt;
                        break;
                    case Dir.Left:
                        position.X -= speed * dt;
                        break;
                    case Dir.Right:
                        position.X += speed * dt;
                        break;

                }
            }
        }
    }
}





