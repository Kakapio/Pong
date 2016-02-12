﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Default variables.
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Variables for 2D sprites.
        Texture2D ball;
        Texture2D player;
        Texture2D midLine;

        //Variables pertaining to the ball's position and its motion.
        Vector2 ballPosition = new Vector2 (640, 360);
        Vector2 ballSpeed = new Vector2(150, 150);

        Vector2 paddlePosition;

        public Game1()
        {
            //Default stuff.
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Changing window size
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            base.Initialize();

            paddlePosition = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - player.Width * 11,
                                         graphics.GraphicsDevice.Viewport.Height - player.Height);

            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = this.Content.Load<Texture2D>("Ball");
            player = this.Content.Load<Texture2D>("Player");
            midLine = this.Content.Load<Texture2D>("MidLine");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Allows game to be exited if ESC is pushed.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Move sprite by speed, scaled by elapsed time.
            ballPosition += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int maxX = GraphicsDevice.Viewport.Width - ball.Width;
            int maxY = GraphicsDevice.Viewport.Height - ball.Height;

            //Check for bounce
            if (ballPosition.X > maxX || ballPosition.X < 0)
                ballSpeed.X *= -1;

            if (ballPosition.Y > maxY || ballPosition.Y < 0)
                ballSpeed.Y *= -1;
            else if (ballPosition.X > maxX)
            {
                //Ball hit left/right of screen, reset ball.
                ballPosition.Y = GraphicsDevice.Viewport.Height / 2;
                ballPosition.X = GraphicsDevice.Viewport.Width / 2;
                ballSpeed.X = 150;
                ballSpeed.Y = 150;
            }

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Down))
                paddlePosition.Y += 5;
            else if (keyState.IsKeyDown(Keys.Up))
                paddlePosition.Y -= 5;

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(midLine, destinationRectangle: new Rectangle(640, 0, 20, 720));
            spriteBatch.Draw(ball, destinationRectangle: new Rectangle ((int)ballPosition.X, (int)ballPosition.Y, 70, 70));
            spriteBatch.Draw(player, paddlePosition);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
