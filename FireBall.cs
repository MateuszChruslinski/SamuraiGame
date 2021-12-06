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
        public int radius = 21;
        public Vector2 direction;
        //public bool helpVal;
        public bool collided = false;
        //Vector2 sumOfAll;

        public FireBall(int newSpeed = 300, float positionX = 100, float positionY = 100, float playerX =0, float playerY=0)
        {
            speed = newSpeed;
            position = new Vector2(positionX, positionY);
            direction = new Vector2(playerX, playerY);

            //helpVal = true;
            //double sinA = Math.Sin(direction.X - position.X / Math.Sqrt((direction.X - position.X) * (direction.X - position.X) + (direction.Y - position.Y) * (direction.Y - position.Y)));
            //double cosA = Math.Cos(direction.Y - position.Y / Math.Sqrt((direction.X - position.X) * (direction.X - position.X) + (direction.Y - position.Y) * (direction.Y - position.Y)));
            //sumOfAll = new Vector2((float)sinA, (float)cosA);
        }

        public void fireBallPositionUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 moveDir = direction - position;
            moveDir = Vector2.Normalize(moveDir);
            position += moveDir * speed * dt;

        }
    }
}
