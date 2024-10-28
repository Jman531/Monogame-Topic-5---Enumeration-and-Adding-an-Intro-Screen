using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_Topic_5___Enumeration_and_Adding_an_Intro_Screen
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        bool justUpdatedScreen = false;

        MouseState mouseState;

        Rectangle window;

        Texture2D greyTribbleTexture, brownTribbleTexture, creamTribbleTexture, orangeTribbleTexture, tribbleIntroTexture;
        Rectangle greyTribbleRect, brownTribbleRect, creamTribbleRect, orangeTribbleRect;

        Vector2 greyTribbleSpeed, brownTribbleSpeed, creamTribbleSpeed, orangeTribbleSpeed;

        SoundEffect tribbleCoo;
        SpriteFont instructionFont;

        Random generator = new Random();

        enum Screen
        {
            Intro,
            TribbleYard
        }

        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Intro;
            greyTribbleRect = new Rectangle(generator.Next(0, 701), generator.Next(0, 501), 100, 100);
            greyTribbleSpeed = new Vector2(2, 2);
            brownTribbleRect = new Rectangle(generator.Next(0, 701), generator.Next(0, 501), 100, 100);
            brownTribbleSpeed = new Vector2(generator.Next(1, 11), 0);
            creamTribbleRect = new Rectangle(generator.Next(0, 701), generator.Next(0, 501), 100, 100);
            creamTribbleSpeed = new Vector2(0, generator.Next(1, 11));
            orangeTribbleRect = new Rectangle(generator.Next(0, 701), generator.Next(0, 501), 100, 100);
            orangeTribbleSpeed = new Vector2(generator.Next(-10, 11), generator.Next(-10, 11));

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            brownTribbleTexture = Content.Load<Texture2D>("tribbleBrown");
            creamTribbleTexture = Content.Load<Texture2D>("tribbleCream");
            orangeTribbleTexture = Content.Load<Texture2D>("tribbleOrange");
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");
            instructionFont = Content.Load<SpriteFont>("instruction");
            tribbleCoo = Content.Load<SoundEffect>("tribble_coo");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.TribbleYard;
                    justUpdatedScreen = true;
                }
            }
            else if (screen == Screen.TribbleYard)
            {
                greyTribbleRect.X += (int)greyTribbleSpeed.X;
                greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
                brownTribbleRect.X += (int)brownTribbleSpeed.X;
                brownTribbleRect.Y += (int)brownTribbleSpeed.Y;
                creamTribbleRect.X += (int)creamTribbleSpeed.X;
                creamTribbleRect.Y += (int)creamTribbleSpeed.Y;
                orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
                orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;

                if (greyTribbleRect.Right >= window.Width || greyTribbleRect.Left <= 0)
                {
                    greyTribbleSpeed.X *= -1;
                    tribbleCoo.Play();
                }
                if (greyTribbleRect.Bottom >= window.Height || greyTribbleRect.Top <= 0)
                {
                    greyTribbleSpeed.Y *= -1;
                    tribbleCoo.Play();
                }

                if (brownTribbleRect.Left >= window.Width)
                    brownTribbleRect.X = -99;

                if (creamTribbleRect.Bottom >= window.Height || creamTribbleRect.Top <= 0)
                {
                    creamTribbleSpeed.Y *= -1;
                    tribbleCoo.Play();
                }

                if (orangeTribbleRect.Right >= window.Width || orangeTribbleRect.Left <= 0)
                {
                    orangeTribbleSpeed.X *= -1;
                    tribbleCoo.Play();
                }
                if (orangeTribbleRect.Bottom >= window.Height || orangeTribbleRect.Top <= 0)
                {
                    orangeTribbleSpeed.Y *= -1;
                    tribbleCoo.Play();
                }

                if (mouseState.LeftButton != ButtonState.Pressed)
                    justUpdatedScreen = false;

                if (mouseState.LeftButton == ButtonState.Pressed && !justUpdatedScreen)
                    this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(instructionFont, "Click to start.", new Vector2(0, 0), Color.Black);
            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
                _spriteBatch.Draw(brownTribbleTexture, brownTribbleRect, Color.White);
                _spriteBatch.Draw(creamTribbleTexture, creamTribbleRect, Color.White);
                _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect, Color.White);
                _spriteBatch.DrawString(instructionFont, "Click to end.", new Vector2(0, 0), Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
