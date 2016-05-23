﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace WizardLizard
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
        public static Dictionary<int, GameObject> spawnList = new Dictionary<int, GameObject>();
        private static List<GameObject> objectToAdd = new List<GameObject>();
        private static List<GameObject> objectsToRemove = new List<GameObject>();
        private static List<GameObject> gameObjects = new List<GameObject>();

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.HardwareModeSwitch = true;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
           // graphics.IsFullScreen = true;
        }

        public static List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
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
            get { return deltaTime; }
        }

        

        public static Vector2 PlayerPos
        {
            get { return playerPos; }

            set { playerPos = value; }
        }

        public static List<GameObject> ObjectsToRemove
        {
            get
            {
                return objectsToRemove;
            }

            set
            {
                objectsToRemove = value;
            }
        }

        public static List<GameObject> ObjectToAdd
        {
            get
            {
                return objectToAdd;
            }

            set
            {
                objectToAdd = value;
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
            GameObject background = new GameObject();
            background.AddComponent(new SpriteRenderer(background, "Level01", 1f));
            background.Transform.Position = new Vector2(0, 0);
            GameObjects.Add(background);
            director = new Director(new AimerBuilder());
            gameObjects.Add(director.Construct(new Vector2(0, 0)));
            director = new Director(new PlayerBuilder());
            gameObjects.Add(director.Construct(new Vector2(10, 10)));
            director = new Director(new PlayerHealthBuilder());
            gameObjects.Add(director.Construct(new Vector2(10, 10)));
            director = new Director(new LeverBuilder());
            gameObjects.Add(director.Construct(new Vector2(50, 100), 1));
            gameObjects.Add(director.Construct(new Vector2(200, 800), 51));
            director = new Director(new DoorBuilder());
            gameObjects.Add(director.Construct(new Vector2(970, 655), 1));
            director = new Director(new CompanionBuilder());
            gameObjects.Add(director.Construct(new Vector2(10, 10)));
            director = new Director(new ArcherBuilder());
            gameObjects.Add(director.Construct(new Vector2(310, 10)));
            director = new Director(new GoblinBuilder());
            gameObjects.Add(director.Construct(new Vector2(480, 250)));
            director = new Director(new OrcBuilder());
            gameObjects.Add(director.Construct(new Vector2(900, 10)));
            director = new Director(new MoveableBoxBuilder());
            gameObjects.Add(director.Construct(new Vector2(600,0)));
            director = new Director(new PlatformBuilder());
            //højre side bund
            gameObjects.Add(director.Construct(new Vector2(0, 850),386,100));
            //venstre side bund
            gameObjects.Add(director.Construct(new Vector2(483, 850), 1117, 100));
            //venstre væg
            gameObjects.Add(director.Construct(new Vector2(0, 75), 60, 775));
            //første platform over hulen
            gameObjects.Add(director.Construct(new Vector2(966, 608), 386, 48));
            //højeste platform over hulen
            gameObjects.Add(director.Construct(new Vector2(1352, 560), 194, 48));
            //højre væg
            gameObjects.Add(director.Construct(new Vector2(1498, 240), 48, 480));
            //venstre stenplatform
            gameObjects.Add(director.Construct(new Vector2(190, 488), 196, 48));

            spawnList.Add(51, director.Construct(new Vector2(482, 535), "MagicPlatform"));


            //To be: non-solidplatforms
            director = new Director(new NonSolidPlatformBuilder());
            //midterste gren
            gameObjects.Add(director.Construct(new Vector2(1350, 240), 148, 40));
            //øverste gren
            gameObjects.Add(director.Construct(new Vector2(1150, 195), 200, 45));
            //nederste gren
            gameObjects.Add(director.Construct(new Vector2(1360, 395), 138, 30));
            //venstre gren
            gameObjects.Add(director.Construct(new Vector2(60, 320), 178, 30));



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

            foreach (GameObject go in objectToAdd)
            {
                go.LoadContent(Content);
            }
            GameObjects.AddRange(ObjectToAdd);
            objectToAdd.Clear();
            foreach (GameObject go in objectsToRemove)
            {
                gameObjects.Remove(go);

            }
            objectsToRemove.Clear();

            foreach (GameObject go in gameObjects)
            {
                go.Update();
            }

            // TODO: Add your update logic here
            base.Update(gameTime);
        }
        

        public void AddGameObject(GameObject go)
        {
            objectToAdd.Add(go);
        }
        public void RemoveGameObject(GameObject go)
        {
            objectsToRemove.Add(go);

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
