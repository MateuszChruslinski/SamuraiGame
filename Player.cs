using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SamuraiGame
{
    class Player
    {

        public bool swordRelease = false;
        public Dir direction = Dir.Down;
        private const int speed = 300;
        static public Vector2 defaultPosition = new Vector2(500, 300);
        private Vector2 position = new Vector2(500, 300);
        public bool activButton = false;
        private double timer = 0.5f;
        public int radious = 48;

        private double timeOfGame = 0;
        private int score = 0;

        public SpriteAnimation anim;
        public SpriteAnimation swordAnim; //
        public SpriteAnimation[] animations = new SpriteAnimation[4];
        public SpriteAnimation[] swordAnimations = new SpriteAnimation[4]; //

        double timerOfActiveSword = 0;


        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public double TimeOfGame
        {
            get => timeOfGame;
            set => timeOfGame = value;
        }

        public int Score
        {
            get => score;
            set => score = value;
        }

        public void Update(GameTime gameTime, bool ingame)
        {
            if (ingame == true)
            {
                timeOfGame += gameTime.ElapsedGameTime.TotalSeconds;

                KeyboardState kState = Keyboard.GetState();
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                activButton = false;

                if (kState.IsKeyDown(Keys.Right))
                {
                    direction = Dir.Right;
                    activButton = true;
                }
                if (kState.IsKeyDown(Keys.Left))
                {
                    direction = Dir.Left;
                    activButton = true;
                }
                if (kState.IsKeyDown(Keys.Up))
                {
                    direction = Dir.Up;
                    activButton = true;
                }
                if (kState.IsKeyDown(Keys.Down))
                {
                    direction = Dir.Down;
                    activButton = true;
                }

                if (activButton == true)
                {
                    anim.Position = new Vector2(position.X - 48, position.Y - 48);
                    anim.Update(gameTime); ///

                    switch (direction)
                    {
                        case Dir.Right:
                            if (position.X < 1200)
                            {
                                position.X += speed * dt;
                            }
                            else
                                break;
                            break;
                        case Dir.Left:
                            if (position.X > 0)
                            {
                                position.X -= speed * dt;
                            }
                            else
                                break;
                            break;
                        case Dir.Down:
                            if (position.Y < 620)
                            {
                                position.Y += speed * dt;
                            }
                            else
                                break;
                            break;
                        case Dir.Up:
                            if (position.Y > 0)
                            {
                                position.Y -= speed * dt;
                            }
                            else
                                break;
                            break;
                    }
                }



                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (kState.IsKeyDown(Keys.Q))
                {

                    if (timer <= 0)
                    {
                        Dart.dart.Add(new Dart(position, direction));
                        timer = 0.5f;
                    }
                }


                timerOfActiveSword -= gameTime.ElapsedGameTime.TotalSeconds;
                if (kState.IsKeyDown(Keys.W) && timerOfActiveSword <= 0)
                {
                    swordAnim.Update(gameTime);
                    swordRelease = true;
                    timerOfActiveSword = 1;
                }
                if(timerOfActiveSword > 0)
                {
                    swordAnim.Update(gameTime);
                    swordRelease = true;
                    timerOfActiveSword -= gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    swordRelease = false;
                    swordAnim.setFrame(0);
                }
                

            }
        }

    }
}
