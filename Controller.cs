using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SamuraiGame
{
    class Controller
    {
        public List<Monster> monster = new List<Monster>();

        double timer = 2;
        public bool ingame = false;
        public bool dead = false;
        MouseState mState;

        public void monsterUpdate(GameTime gameTime, List<Texture2D> spriteToMonster)
        {
            if (ingame == true)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    monster.Add(new Monster1(spriteToMonster));
                    monster.Add(new Monster2(spriteToMonster));
                    timer = 2;
                }
            }

            if (ingame == false && dead == false)
            {
                mState = Mouse.GetState();
                int mouseX = mState.X;
                int mouseT = mState.Y;

                if (mState.LeftButton == ButtonState.Pressed)
                {
                    if (mState.X >= 100 && mState.X <= 372 && mState.Y >= 100 && mState.Y <= 203) //Setting the button to start the game
                    {
                        ingame = true;
                        timer = 2;
                    }
                }

                if (mState.LeftButton == ButtonState.Pressed)
                {
                    if (mState.X >= 100 && mState.X <= 372 && mState.Y >= 250 && mState.Y <= 353) //Setting the button to exit the game
                    {
                        System.Environment.Exit(0);
                    }
                }
            }
        }
    }
}
