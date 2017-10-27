using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player Player1;
        Texture2D background;
        KeyboardState previousState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            Player1 = new Player("Naruto" , graphics);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            previousState = Keyboard.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Naruto/Hidden_Leaf");
            Player1.LoadPlayer(Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-spesific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.DeepSkyBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            //if (gameTime.TotalGameTime.Seconds > Player1.GetAttack().GetComboStart() + 1 && Player1.GetAttack().GetComboStart() != 0)
            //{
            //    Console.WriteLine(Player1.GetAttack().GetClicked());
            //    Player1.GetAttack().Reset();
            //}
            if ((Keyboard.GetState().IsKeyDown(Keys.Right)))
            {
                if (previousState.IsKeyUp(Keys.Right) && Player1.GetLastMove() == 'l')
                {
                    Player1.StanceRight(spriteBatch);
                    Player1.SetLastMove('r');
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    Player1.RunRight(spriteBatch);
                else
                    Player1.WalkRight(spriteBatch);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (previousState.IsKeyUp(Keys.Right) && Player1.GetLastMove() == 'r')
                {
                    Player1.StanceRight(spriteBatch);
                    Player1.SetLastMove('l');
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    Player1.RunLeft(spriteBatch);
                else
                    Player1.WalkLeft(spriteBatch);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Player1.GetLastMove() == 'r')
                    Player1.CrouchRight(spriteBatch);
                else
                    Player1.CrouchLeft(spriteBatch);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) || Player1.IsInBasicCombo(gameTime))
            {
                if (previousState.IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.A) && Player1.GetAttack().GetClicked() < Player1.GetAttack().GetComboLength() - 1)
                    Player1.GetAttack().SetClicked(Player1.GetAttack().GetClicked() + 1);
                Player1.BasicCombo(spriteBatch, gameTime, Player1.GetAttack().GetClicked());
                
            }
            else if ((Keyboard.GetState().IsKeyDown(Keys.B)) && (Keyboard.GetState().IsKeyDown(Keys.NumPad1)) && (Player1.GetSpecialAttTimer() + 10 < gameTime.TotalGameTime.Seconds))
            {
                Player1.SpecialAttack(spriteBatch, gameTime);
                System.Threading.Thread.Sleep(25);
            }
            else // No key pressed
            {
                if (Player1.GetLastMove() == 'r')
                    Player1.StanceRight(spriteBatch);
                else
                    Player1.StanceLeft(spriteBatch);
            }
            spriteBatch.End();
            previousState = Keyboard.GetState();
            System.Threading.Thread.Sleep(75);
            base.Draw(gameTime);
        }
    }
}
