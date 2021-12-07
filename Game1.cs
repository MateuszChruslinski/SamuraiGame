using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SamuraiGame
{
    enum Dir
    {
        Down,
        Up,
        Left,
        Right
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        float rotation;
        int vektorX;
        int vektorY;
        float checkDist;

        // Fonts
        SpriteFont gameFont;

        //Screens
        Texture2D menuScreen;
        Texture2D gameOverScreen;
        Texture2D buttonExit;
        Texture2D buttonNewGame;
        Texture2D enterToContinue;

        //Arenas
        Texture2D arena2;

        //Player with animations
        Texture2D ninjaAnimUp;
        Texture2D ninjaAnimDown;
        Texture2D ninjaAnimLeft;
        Texture2D ninjaAnimRight;

        //Monsters
        Texture2D blueSkullAnim;
        Texture2D purpleSkullAnim;
        Texture2D oni;

        //Weapons and utilities elements
        Texture2D knife;
        Texture2D dart;
        Texture2D heart;
        Texture2D heartEmpty;
        Texture2D fireBallTexture;

        //Creating classes
        Player player = new Player();
        Controller controller = new Controller();
        FireBall fireBall = new FireBall();

        List<Texture2D> spritesToMonster = new List<Texture2D>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //setting resolution
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Fons
            gameFont = Content.Load<SpriteFont>("Font/galleryFont");

            //Screens
            gameOverScreen = Content.Load<Texture2D>("Screen/gameOverScreen");
            menuScreen = Content.Load<Texture2D>("Screen/menuScreen");
            buttonExit = Content.Load<Texture2D>("Screen/buttonExit");
            buttonNewGame = Content.Load<Texture2D>("Screen/buttonNewGame");
            enterToContinue = Content.Load<Texture2D>("Screen/enterToContinue");

            //Arenas
            arena2 = Content.Load<Texture2D>("Arena/arena");

            //Player with animations
            ninjaAnimUp = Content.Load<Texture2D>("Player/ninjaUp");
            ninjaAnimDown = Content.Load<Texture2D>("Player/ninjaDown");
            ninjaAnimLeft = Content.Load<Texture2D>("Player/ninjaLeft");
            ninjaAnimRight = Content.Load<Texture2D>("Player/ninjaRight");

            //Monsters
            blueSkullAnim = Content.Load<Texture2D>("Monsters/blueSkullAnim");
            purpleSkullAnim = Content.Load<Texture2D>("Monsters/purpleSkullAnim");
            oni = Content.Load<Texture2D>("Monsters/oni");

            //Weapons and utilities elements
            knife = Content.Load<Texture2D>("Player/knife");
            dart = Content.Load<Texture2D>("Player/dart");
            heart = Content.Load<Texture2D>("Player/heart");
            heartEmpty = Content.Load<Texture2D>("Player/heartEmpty");
            fireBallTexture = Content.Load<Texture2D>("Monsters/fireBall");

            //Defining an player animations
            player.animations[0] = new SpriteAnimation(ninjaAnimDown, 4, 8);
            player.animations[1] = new SpriteAnimation(ninjaAnimUp, 4, 8);
            player.animations[2] = new SpriteAnimation(ninjaAnimLeft, 4, 8);
            player.animations[3] = new SpriteAnimation(ninjaAnimRight, 4, 8);

            
            spritesToMonster.Add(blueSkullAnim);
            spritesToMonster.Add(purpleSkullAnim);
            spritesToMonster.Add(oni);
            //spritesToMonster[0] = blueSkullAnim;
            //spritesToMonster[1] = purpleSkullAnim;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Game timer
            // Upgrade player position
            player.Update(gameTime, controller.ingame);
            controller.monsterUpdate(gameTime, spritesToMonster);

            
            foreach (FireBall fireBall in controller.fireBall)
            {
                fireBall.fireBallPositionUpdate(gameTime); ///

            }
            
            foreach (Monster monst in controller.monster)
            {
                if (monst.eventTime > 0)
                {
                    monst.eventTime -= gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    if (monst.typeOfMonster == 3)
                    {
                        controller.fireBallUpdate(gameTime, monst.position.X, monst.position.Y, player.Position.X, player.Position.Y);
                        monst.eventTime = 4;
                    }
                }

            }
            // Upgrade dart position
            foreach (Dart proj in Dart.dart)
            {
                proj.dartUpdate(gameTime, controller.ingame);
            }


            for (int i = 0; i < controller.monster.Count; i++)
            {
                controller.monster[i].positionUpdate(gameTime, player.Position, controller.monster[i].speed, controller.monster[i].typeOfMonster);
            }


            /////////// Checking for player collision

            foreach (Monster monst in controller.monster)
            {
                float checkDist = Vector2.Distance(monst.position, player.Position);
                if (checkDist < player.radious)
                {
                    controller.ingame = false;
                    controller.dead = true;
                    Dart.dart.Clear();
                    controller.monster.Clear();
                    controller.fireBall.Clear();
                    player.Position = Player.defaultPosition; 

                    break;

                }
            }
            // Checking for fireBall collision

            
            foreach (FireBall fireBall in controller.fireBall)
            {
                float checkDist = Vector2.Distance(fireBall.position, player.Position + new Vector2(player.radious, player.radious));
                if(checkDist < player.radious)
                {
                    controller.ingame = false;
                    controller.dead = true;
                    Dart.dart.Clear();
                    controller.monster.Clear();
                    controller.fireBall.Clear();
                    player.Position = Player.defaultPosition;
                    break;
                }
            }
            
            /////////// Chcecking sword and monster collision
            foreach (Monster monst in controller.monster)
            {

                switch (player.direction) // 40 pix lenght of sword
                {
                    case Dir.Right:
                        checkDist = Vector2.Distance(monst.position, new Vector2(player.Position.X + 40, player.Position.Y));
                        break;
                    case Dir.Left:
                        checkDist = Vector2.Distance(monst.position, new Vector2(player.Position.X + -40, player.Position.Y));
                        break;
                    case Dir.Up:
                        checkDist = Vector2.Distance(monst.position, new Vector2(player.Position.X, player.Position.Y - 40));
                        break;
                    case Dir.Down:
                        checkDist = Vector2.Distance(monst.position, new Vector2(player.Position.X, player.Position.Y + 40));
                        break;
                }
                if (checkDist < monst.radius / 2 + 40 && player.swordRelease && true) //Lenght of sword
                {
                    if (monst.monsterHp > 0)
                    {
                        monst.monsterHp--;
                    }
                    if (monst.monsterHp == 0)
                    {
                        monst.collided = true;
                        player.Score += 1;
                    }
                }
            }

            ///////// Chceking darts and monsters collision

            foreach (Monster monst in controller.monster)
            {
                foreach (Dart proj in Dart.dart)
                {
                    float checkDist = Vector2.Distance(monst.position, proj.Position);
                    if (checkDist < monst.radius / 2 + 16) // 16 - half lenght of dart
                    {
                        if (monst.monsterHp > 0)
                        {
                            proj.collided = true;
                            monst.monsterHp--;
                        }
                        if (monst.monsterHp == 0)
                        {
                            proj.collided = true;
                            monst.collided = true;
                            player.Score += 1;
                        }


                    }
                }
            }
            // Removal from list

            controller.monster.RemoveAll(p => p.collided);
            Dart.dart.RemoveAll(p => p.collided);
            controller.fireBall.RemoveAll(p => p.collided);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();


            // Drawing while game is on
            if (controller.ingame == true)
            {

                //Arena Display
                _spriteBatch.Draw(arena2, new Vector2(0, 0), Color.White);

                //Fonst Display
                _spriteBatch.DrawString(gameFont, "Time: " + Math.Ceiling(player.TimeOfGame).ToString() + " Seconds", new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(gameFont, "Score: " + (player.Score).ToString() + " Points", new Vector2(0, 25), Color.White);

                for (int i = 0; i < controller.fireBall.Count; i++)
                {
                    _spriteBatch.Draw(fireBallTexture, new Vector2(controller.fireBall[i].position.X - controller.fireBall[i].radius, controller.fireBall[i].position.Y - controller.fireBall[i].radius), Color.White);
                }

                //Player Display
                if (player.direction == Dir.Down)
                {
                    player.anim = player.animations[0];
                    player.anim.Draw(_spriteBatch, new Vector2(player.Position.X, player.Position.Y));
                }
                if (player.direction == Dir.Up)
                {
                    player.anim = player.animations[1];
                    player.anim.Draw(_spriteBatch, new Vector2(player.Position.X, player.Position.Y));
                }
                if (player.direction == Dir.Left)
                {
                    player.anim = player.animations[2];
                    player.anim.Draw(_spriteBatch, new Vector2(player.Position.X, player.Position.Y));
                }
                if (player.direction == Dir.Right)
                {
                    player.anim = player.animations[3];
                    player.anim.Draw(_spriteBatch, new Vector2(player.Position.X, player.Position.Y));
                }


                //Darts Display
                foreach (Dart proj in Dart.dart)
                {

                    Vector2 spriteOrigin;
                    spriteOrigin = new Vector2(dart.Width / 2, dart.Height / 2);

                    //Define dart rotation
                    switch (proj.Direction)
                    {
                        case Dir.Right:
                            rotation = 0;
                            break;
                        case Dir.Down:
                            rotation = 1.570796326795f;
                            break;
                        case Dir.Left:
                            rotation = 3.14159265359f;
                            break;
                        case Dir.Up:
                            rotation = 4.712388980385f;
                            break;
                    }
                    _spriteBatch.Draw(dart, new Vector2(proj.Position.X + 48, proj.Position.Y + 48), null, Color.White, rotation, spriteOrigin, 1f, SpriteEffects.None, 0);
                }

                //Sword Display
                if (player.swordRelease == true)
                {
                    Vector2 spriteOrigin;
                    spriteOrigin = new Vector2(0, 0);

                    switch (player.direction)
                    {
                        case Dir.Right:
                            rotation = 0;
                            vektorX = 48;
                            vektorY = 48;
                            break;
                        case Dir.Down:
                            rotation = 1.570796326795f;
                            vektorX = 48 + 8;
                            vektorY = 48;
                            break;
                        case Dir.Left:
                            rotation = 3.14159265359f;
                            vektorX = 48;
                            vektorY = 48 + 16;
                            break;
                        case Dir.Up:
                            rotation = 4.712388980385f;
                            vektorX = 48 - 8;
                            vektorY = 48;
                            break;
                    }
                    _spriteBatch.Draw(knife, new Vector2(player.Position.X + vektorX, player.Position.Y + vektorY), null, Color.White, rotation, spriteOrigin, 1f, SpriteEffects.None, 0);
                    player.swordRelease = false;
                }

                foreach (Monster monst in controller.monster)
                {
                    if (monst.typeOfMonster == 1)
                    {
                        monst.Update(gameTime);
                        _spriteBatch.Draw(heart, new Vector2(monst.position.X, monst.position.Y - 20), Color.White);
                        monst.anim.Draw(_spriteBatch, new Vector2(monst.position.X, monst.position.Y));
                    }
                    if (monst.typeOfMonster == 2)
                    {
                        monst.Update(gameTime);
                        if (monst.monsterHp == 2)
                        {
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X + 22, monst.position.Y - 20), Color.White);
                        }
                        if (monst.monsterHp == 1)
                        {
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heartEmpty, new Vector2(monst.position.X + 22, monst.position.Y - 20), Color.White);
                        }

                        monst.anim.Draw(_spriteBatch, new Vector2(monst.position.X, monst.position.Y));
                    }
                    if (monst.typeOfMonster == 3)
                    {
                        monst.Update(gameTime);
                        if (monst.monsterHp == 3)
                        {
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X + 22, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X + 44, monst.position.Y - 20), Color.White);
                        }
                        if (monst.monsterHp == 2)
                        {
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X + 22, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heartEmpty, new Vector2(monst.position.X + 44, monst.position.Y - 20), Color.White);
                        }
                        if (monst.monsterHp == 1)
                        {
                            _spriteBatch.Draw(heart, new Vector2(monst.position.X, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heartEmpty, new Vector2(monst.position.X + 22, monst.position.Y - 20), Color.White);
                            _spriteBatch.Draw(heartEmpty, new Vector2(monst.position.X + 44, monst.position.Y - 20), Color.White);
                        }

                        monst.anim.Draw(_spriteBatch, new Vector2(monst.position.X, monst.position.Y));
                    }

                }
            }

            // Start screen display
            if (controller.ingame == false && controller.dead == false)
            {
                _spriteBatch.Draw(menuScreen, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(buttonNewGame, new Vector2(100, 100), Color.White);
                _spriteBatch.Draw(buttonExit, new Vector2(100, 250), Color.White);
            }

            // GameOver Display
            if (controller.ingame == false && controller.dead == true)
            {
                _spriteBatch.Draw(gameOverScreen, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(enterToContinue, new Vector2(900, 350), Color.White);
                //230x88 (1)
                KeyboardState kState = Keyboard.GetState();

                _spriteBatch.DrawString(gameFont, "Player statistics:", new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(gameFont, "Game Time: " + Math.Ceiling(player.TimeOfGame).ToString() + " Seconds", new Vector2(0, 50), Color.White);
                _spriteBatch.DrawString(gameFont, "Score: " + (player.Score).ToString() + " Points", new Vector2(0, 75), Color.White);

                if (kState.IsKeyDown(Keys.Enter))
                {
                    controller.dead = false;
                    player.TimeOfGame = 0;
                    player.Score = 0;
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
