using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SamuraiGame
{
    class FireBall
    {
        public Vector2 position = new Vector2(600, 300);
        public int speed;
        public int radius = 10;
        public Vector2 direction;
        public bool collided = false;
        Vector2 sumOfAll;

        public int playerRadious = 48;

        public FireBall(int newSpeed = 300, float positionX = 100, float positionY = 100, float playerX =0, float playerY=0)
        {
            speed = newSpeed;
            position = new Vector2(positionX, positionY);
            direction = new Vector2(playerX, playerY);
            double sinA = (position.X - (direction.X + playerRadious)) / Math.Sqrt(Math.Abs(position.X - (direction.X + playerRadious)) * Math.Abs(position.X - (direction.X + playerRadious)) + Math.Abs(position.Y - (direction.Y + playerRadious)) * Math.Abs(position.Y - (direction.Y + playerRadious)));
            double cosA = (position.Y - (direction.Y + playerRadious)) / Math.Sqrt(Math.Abs(position.X - (direction.X + playerRadious)) * Math.Abs(position.X - (direction.X + playerRadious)) + Math.Abs(position.Y - (direction.Y + playerRadious)) * Math.Abs(position.Y - (direction.Y + playerRadious)));
            sumOfAll = new Vector2((float)sinA, (float)cosA);
        }


        public void fireBallPositionUpdate(GameTime gameTime)
        {
            int addFrame = 50;
            // Removing items from list, to restore memeory
            if (position.X > 1280 + addFrame || position.X < 0 - addFrame || position.Y > 720 + addFrame || position.Y < 0 - addFrame)
            {
                collided = true;
            }

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position -= sumOfAll * speed * dt;

        }
    }
}
