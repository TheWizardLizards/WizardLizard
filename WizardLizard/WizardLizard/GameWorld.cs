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
        enum GameState
        {
            MainMenu,
            Playing
        }
        GameState currentGameState = GameState.MainMenu;
        bool paused = false;
        bool canInitialize = true;
        Rectangle pausedRectangle = new Rectangle(0, 0, 1600, 900);
        Button btnStartGame;
        Button btnContinue;
        Button btnExit;
        Button btnMainMenu;
        Button btnSave;
        Button btnLoad;
        Button btnCreateProfile;
        Button btnLoadProfile;
        LevelBuilder levelBuilder = new LevelBuilder();
        private string level = "level01";
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
            if (currentGameState == GameState.Playing)
            {
                if (level == "level01")
                {
                    levelBuilder.LevelOne();
                    canInitialize = false;
                }
                if (level == "level02")
                {
                    levelBuilder.LevelTwo();
                    canInitialize = false;
                }
                IsMouseVisible = false;
            }



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
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    IsMouseVisible = true;
                    btnCreateProfile = new Button(Content.Load<Texture2D>("CreateProfileOff"), new Vector2(100, 300), "CreateProfileOff", "CreateProfileOn");
                    btnLoadProfile = new Button(Content.Load<Texture2D>("LoadProfileOff"), new Vector2(100, 400), "LoadProfileOff", "LoadProfileOn");
                    btnStartGame = new Button(Content.Load<Texture2D>("PlayOff"), new Vector2(100, 500), "PlayOff", "PlayOn");
                    btnLoad = new Button(Content.Load<Texture2D>("LoadOff"), new Vector2(100, 600), "LoadOff", "LoadOn");
                    btnExit = new Button(Content.Load<Texture2D>("ExitOff"), new Vector2(100, 700), "ExitOff", "ExitOn");
                    break;
                case GameState.Playing:
                    if (!paused)
                    {
                        foreach (GameObject go in gameObjects)
                        {
                            go.LoadContent(Content);
                        }
                    }
                    break;
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
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    //objectsToRemove.AddRange(gameObjects);
                    //foreach (GameObject go in objectsToRemove)
                    //{
                    //    gameObjects.Remove(go);
                    //}
                    //objectsToRemove.Clear();
                    btnStartGame.Update(Content, mouse);
                    btnExit.Update(Content, mouse);
                    btnLoad.Update(Content, mouse); //Mangler funktionalitet
                    btnCreateProfile.Update(Content, mouse); //Mangler funktionalitet
                    btnLoadProfile.Update(Content, mouse); //Mangler funktionalitet
                    if (btnStartGame.isClicked)
                    {
                        currentGameState = GameState.Playing;
                    }
                    if (btnExit.isClicked)
                    {
                        Exit();
                    }
                    break;
                case GameState.Playing:
                    if (!paused)
                    {
                        IsMouseVisible = false;
                        foreach (GameObject go in objectsToRemove)
                        {
                            gameObjects.Remove(go);
                        }
                        objectsToRemove.Clear();
                        if (keyState.IsKeyDown(Keys.Escape) || keyState.IsKeyDown(Keys.P))
                        {
                            paused = true;
                        }
                        foreach (GameObject go in objectToAdd)
                        {
                            go.LoadContent(Content);
                        }
                        GameObjects.AddRange(ObjectToAdd);
                        objectToAdd.Clear();

                        foreach (GameObject go in gameObjects)
                        {
                            go.Update();
                        }
                        if (canInitialize)
                        {
                            Initialize();
                            canInitialize = false;
                        }
                        if (playerPos.X > 1550 && playerPos.Y > 750 && level == "level01")
                        {
                            objectsToRemove.AddRange(gameObjects);
                            level = "level02";
                            canInitialize = true;
                        }
                    }
                    if (paused)
                    {
                        IsMouseVisible = true;
                        btnContinue = new Button(Content.Load<Texture2D>("ContinueOff"), new Vector2(700, 300), "ContinueOff", "ContinueOn");
                        btnSave = new Button(Content.Load<Texture2D>("SaveOff"), new Vector2(700, 400), "SaveOff", "SaveOn");
                        btnMainMenu = new Button(Content.Load<Texture2D>("MainMenuOff"), new Vector2(700, 500), "MainMenuOff", "MainMenuOn");
                        btnMainMenu.Update(Content, mouse);
                        btnContinue.Update(Content, mouse);
                        btnSave.Update(Content, mouse); //Mangler funktionalitet
                        if (btnContinue.isClicked)
                        {
                            paused = false;
                        }
                        if (btnMainMenu.isClicked)
                        {
                            paused = false;
                            //canInitialize = true;
                            currentGameState = GameState.MainMenu;
                        }
                    }
                    break;
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
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, 1600, 900), Color.White);
                    btnExit.Draw(spriteBatch);
                    btnStartGame.Draw(spriteBatch);
                    btnLoad.Draw(spriteBatch);
                    btnCreateProfile.Draw(spriteBatch);
                    btnLoadProfile.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    if (!paused)
                    {
                        foreach (GameObject go in gameObjects)
                        {
                            go.Draw(spriteBatch);
                        }
                    }
                    if (paused)
                    {
                        foreach (GameObject go in gameObjects)
                        {
                            go.Draw(spriteBatch);
                        }
                        spriteBatch.Draw(Content.Load<Texture2D>("Paused"), pausedRectangle, Color.White);
                        btnMainMenu.Draw(spriteBatch);
                        btnContinue.Draw(spriteBatch);

                    }
                    break;
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
