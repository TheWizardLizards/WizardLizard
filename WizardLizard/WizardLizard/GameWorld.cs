using Microsoft.Xna.Framework;
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
            director = new Director(new DoorBuilder());
            gameObjects.Add(director.Construct(new Vector2(500, 300), 1));
            director = new Director(new PetBuilder());
            gameObjects.Add(director.Construct(new Vector2(10, 10)));
            director = new Director(new ArcherBuilder());
            gameObjects.Add(director.Construct(new Vector2(310, 10)));
            director = new Director(new GoblinBuilder());
            gameObjects.Add(director.Construct(new Vector2(480, 250)));
            director = new Director(new OrcBuilder());
            gameObjects.Add(director.Construct(new Vector2(900, 10)));
            director = new Director(new PlatformBuilder());
            gameObjects.Add(director.Construct(new Vector2(0, 850),386,100));
            gameObjects.Add(director.Construct(new Vector2(483, 850), 1014, 100));
            gameObjects.Add(director.Construct(new Vector2(0, 75), 48, 775));
            gameObjects.Add(director.Construct(new Vector2(966, 608), 386, 48));
            gameObjects.Add(director.Construct(new Vector2(1352, 560), 194, 48));

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
            foreach (GameObject go in GameObjects)
            {
                if (go.GetComponent("Player") != null)
                {
                    PlayerPos = go.Transform.Position;
                }
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
