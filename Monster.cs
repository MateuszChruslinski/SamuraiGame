using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SamuraiGame
{
    class Monster
    {
        public Vector2 position;
        public int radius;
        public bool collided = false;
        public int typeOfMonster;
        public float speed;
        public int monsterHp;
        public SpriteAnimation anim;

        public void Update(GameTime gameTime)
        {
            anim.Update(gameTime);
        }

        public void positionUpdate(GameTime gameTime, Vector2 playerPos, float speedMonster)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 moveDir = playerPos - position;
            moveDir.Normalize();
            position += moveDir * speedMonster;
        }
    }


    class Monster1 : Monster
    {
        int randX;
        int randY;


        public Monster1(List<Texture2D> spriteToMonster)//
        {
            typeOfMonster = 1;
            monsterHp = 1;
            Random rand = new Random();
            radius = 64;
            speed = 1.5f;
            anim = new SpriteAnimation(spriteToMonster[0], 4, 4);

            int helpX = rand.Next(0, 100);
            if (helpX >= 0 && helpX < 25)
            {
                randX = rand.Next(-50, 1330);
                randY = rand.Next(-100, -50);
            }
            if (helpX >= 25 && helpX < 50)
            {
                randX = rand.Next(-50, 1330);
                randY = rand.Next(770, 820);
            }

            if (helpX >= 50 && helpX < 75)
            {
                randX = rand.Next(-100, -50);
                randY = rand.Next(-50, 770);
            }
            if (helpX >= 75 && helpX < 100)
            {
                randX = rand.Next(1330, 1380);
                randY = rand.Next(-50, 770);
            }

            position = new Vector2(randX, randY);

        }
    }
    class Monster2 : Monster
    {
        int randX;
        int randY;

        public Monster2(List<Texture2D> spriteToMonster)
        {
            typeOfMonster = 2;
            monsterHp = 2;
            Random rand = new Random();
            radius = 90;
            speed = 1;
            anim = new SpriteAnimation(spriteToMonster[1], 4, 4);

            int helpX = rand.Next(0, 100);
            if (helpX >= 0 && helpX < 25)
            {
                randX = rand.Next(-50, 1330);
                randY = rand.Next(-100, -50);
            }
            if (helpX >= 25 && helpX < 50)
            {
                randX = rand.Next(-50, 1330);
                randY = rand.Next(770, 820);
            }

            if (helpX >= 50 && helpX < 75)
            {
                randX = rand.Next(-100, -50);
                randY = rand.Next(-50, 770);
            }
            if (helpX >= 75 && helpX < 100)
            {
                randX = rand.Next(1330, 1380);
                randY = rand.Next(-50, 770);
            }

            position = new Vector2(randX, randY);
        }
    }
}
