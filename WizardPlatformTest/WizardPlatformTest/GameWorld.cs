﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace WizardPlatformTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        Director director;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static Vector2 playerPos;
        private static GameWorld instance;
        private static float deltaTime;
        private static List<GameObject> goToAdd = new List<GameObject>();
        private static List<GameObject> gameObjects = new List<GameObject>();

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public static List<GameObject> GameObjects
        {
            get
            {
                return gameObjects;
            }

            set
            {
                gameObjects = value;
            }
        }

        public List<Collider> Colliders
        {
            get
            {
                List<Collider> tmp = new List<Collider>();

                foreach (GameObject go in GameObjects)
                {
                    tmp.Add(go.GetComponent<Collider>());
                }
                return tmp;

            }
        }

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        public static float DeltaTime
        {
            get
            {
                return deltaTime;
            }
        }

        public static Vector2 PlayerPos
        {
            get
            {
                return playerPos;
            }

            set
            {
                playerPos = value;
            }
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
            director = new Director(new PlayerBuilder());
            gameObjects.Add(director.Construct(new Vector2(10, 10)));
            
            director = new Director(new PetBuilder());
            gameObjects.Add(director.Construct(new Vector2(10, 10)));

            director = new Director(new PlatformBuilder());
            GameObjects.Add(director.Construct(new Vector2(50,400)));
            GameObjects.Add(director.Construct(new Vector2(90, 400)));
            GameObjects.Add(director.Construct(new Vector2(130, 400)));
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
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameObject go in goToAdd)
            {
                go.LoadContent(Content);
            }
            foreach (GameObject go in gameObjects)
            {
                go.Update();
            }
            foreach (GameObject go in GameObjects)
            {
                if(go.GetComponent("Player") != null)
                {
                    PlayerPos = go.Transform.Position;
                }
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
