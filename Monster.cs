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
        double timer = 0;
        public double eventTime;

        public Vector2 createSpawnPoint()
        {
            int randX;
            int randY;
            Random rand = new Random();

            int helpX = rand.Next(0, 100);
            if (helpX >= 0 && helpX < 25)
            {
                randX = rand.Next(-50, 1330);
                randY = rand.Next(-100, -50);
            }
            else if (helpX >= 25 && helpX < 50)
            {
                randX = rand.Next(-50, 1330);
                randY = rand.Next(770, 820);
            }

            else if (helpX >= 50 && helpX < 75)
            {
                randX = rand.Next(-100, -50);
                randY = rand.Next(-50, 770);
            }
            //if (helpX >= 75 && helpX < 100)
            else
            {
                randX = rand.Next(1330, 1380);
                randY = rand.Next(-50, 770);
            }
            Vector2 position;
            position = new Vector2(randX, randY);
            return position;
        }

        public void Update(GameTime gameTime)
        {
            anim.Update(gameTime);
        }

        public void positionUpdate(GameTime gameTime, Vector2 playerPos, float speedMonster, int monsterType)
        {

            

            if (monsterType == 1 || monsterType == 2 )
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 moveDir = playerPos - position;
                moveDir.Normalize();
                position += moveDir * speedMonster;
            }

            if (monsterType == 3)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                float checkXDist = Math.Abs(Math.Abs(playerPos.X) - Math.Abs(position.X));
                float checkYDist = Math.Abs(Math.Abs(playerPos.Y) - Math.Abs(position.Y));
                int distanceOfEscape = 250;

                if (checkXDist < distanceOfEscape && checkYDist < distanceOfEscape)
                {
                    timer = 2;
                }
                if (timer > 0)
                {
                    Vector2 moveDir = playerPos - position;
                    moveDir.Normalize();
                    position -= moveDir * speedMonster;
                }
                if (timer <= 0)
                {
                    Vector2 moveDir = playerPos - position;
                    moveDir.Normalize();
                    position += moveDir * speedMonster;
                }
            }
        }
    }


    class Monster1 : Monster
    {
        public Monster1(List<Texture2D> spriteToMonster)//
        {
            typeOfMonster = 1;
            monsterHp = 1;
            Random rand = new Random();
            radius = 64;
            speed = 1.5f;
            anim = new SpriteAnimation(spriteToMonster[0], 4, 4);
            position = createSpawnPoint();

        }
    }
    class Monster2 : Monster
    {
        public Monster2(List<Texture2D> spriteToMonster)
        {
            typeOfMonster = 2;
            monsterHp = 2;
            Random rand = new Random();
            radius = 90;
            speed = 1;
            anim = new SpriteAnimation(spriteToMonster[1], 4, 4);
            position = createSpawnPoint();
        }
    }

    class Monster3 : Monster
    {
        public Monster3(List<Texture2D> spriteToMonster)//
        {
            typeOfMonster = 3;
            monsterHp = 3;
            Random rand = new Random();
            radius = 64;
            speed = 0.8f;
            anim = new SpriteAnimation(spriteToMonster[2], 7, 4);
            position = createSpawnPoint();
            eventTime = 2;


        }




    }


}
