using Panacea.Interfaces;
using Panacea.Managers;
using Panacea.UserEventArgs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Panacea
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        #region FIELDS
        // DECLARE a public static Boolean, call it 'running':
        public static Boolean running;

        // DECLARE a GraphicsDeviceManager, call it 'graphics':
        private GraphicsDeviceManager graphics;
        // DECLARE a SpriteBatch, call it 'spriteBatch':
        private SpriteBatch spriteBatch;
        // DECLARE an instance of GameContent, call it 'gameContent':
        private GameContent gameContent;

        // DECLARE an EntityManager, call it 'eManager'. Store it as its interface IEntityManager:
        private IEntityManager eManager;
        // DECLARE a SceneManager, call it 'sManager'. Store it as its interface ISceneManager:
        private ISceneManager sManager;
        // DECLARE a CollisionManager, call it 'cManager'. Store it as its interface ICollisionManager:
        private ICollisionManager cManager;
        // DECLARE an InputManager, call it 'iManager'. Store it as its interface IInputManager:
        private IInputManager iManager;

        // DECLARE a public static int to represent the Screen Width, call it 'SCREEN_WIDTH':
        public static int SCREEN_WIDTH;
        // DECLARE a public static int to represent the Screen Height, call it 'SCREEN_HEIGHT':
        public static int SCREEN_HEIGHT;
        #endregion

        /// <summary>
        /// Constructor for objects of class Kernel.
        /// </summary>
        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // SET screen height to 900:
            graphics.PreferredBackBufferHeight = 900;
            // SET screen height to 1600:
            graphics.PreferredBackBufferWidth = 1600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // GET screenHeight:
            SCREEN_HEIGHT = GraphicsDevice.Viewport.Height;

            // GET screenWidth:
            SCREEN_WIDTH = GraphicsDevice.Viewport.Width;

            // INITIALIZE the Managers:
            eManager = new EntityManager();
            sManager = new SceneManager();
            cManager = new CollisionManager();
            iManager = new InputManager();

            base.Initialize();
            running = true;
        }

        /// <summary>
        /// Event handler for the event OnEntityTermination, fired from the Sam class. This will be triggered each time a Sam goes out of play (touches the right or left wall).
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Details about the event.</param>
        private void OnEntityTermination(object sender, OnEntityTerminationEventArgs eventInformation)
        {
            // UNSUBSCRIBE from the event published by the Sam about to be terminated:
            (sender as Sam).OnEntityTermination -= OnEntityTermination;
            // REMOVE the entity from the Collision Manager:
            cManager.removeCollidable(eventInformation.EntityUName, eventInformation.EntityUID);
            // DESPAWN the entity from the Scene Manager:
            sManager.despawn(eventInformation.EntityUName, eventInformation.EntityUID);
            // DESTROY the entity in the Entity Manager:
            eManager.destroyEntity(eventInformation.EntityUName, eventInformation.EntityUID);

            // CREATE a new 'Sam' object with the EntityManager:
            IEntity newBall = eManager.createEntity<Sam>();
            // SPAWN the entity into the SceneGraph:
            sManager.spawn(newBall);
            // ADD the entity to the CollisionManager:
            cManager.addCollidable((newBall as ICollidable));
            // SUBSCIRBE to the termination event that newBall publishes:
            (newBall as Sam).OnEntityTermination += OnEntityTermination;

            // SERVE the new Sam:
            (newBall as Sam).Serve();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // CREATE a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // INITIALIZE 'gameContent' passing in the ContentManager. This will load the game textures:
            gameContent = new GameContent(Content);
            // SPAWN the game objects:
            this.SpawnObjects();
        }

        /// <summary>
        /// SpawnObjects is used to spawn all game objects into the SceneGraph/EntityPool/CollisionMap. Called in Kernel from LoadContent().
        /// </summary>
        private void SpawnObjects()
        {
            // REQUEST a new 'Sam' object from the EntityManager, and pass it to the SceneManager:
            sManager.spawn(eManager.createEntity<Sam>());

            // ITERATE through the SceneGraph:
            for (int i = 0; i < (sManager as SceneManager).SceneGraph.Count; i++)
            {
                // IF the object in the SceneGraph is a 'Sam', call its 'Serve()' method:
                if ((sManager as SceneManager).SceneGraph[i] is Sam)
                {
                    // SERVE the 'Sam' passing in the screenWidth and screenHeight:
                    ((sManager as SceneManager).SceneGraph[i] as Sam).Serve();
                    // SUBSCRIBE to the event that is published in the Sam:
                    ((sManager as SceneManager).SceneGraph[i] as Sam).OnEntityTermination += OnEntityTermination;
                    // SUBSCRIBE the paddle to listen for input events and key release events:
                    (iManager as InputManager).subscribe(((sManager as SceneManager).SceneGraph[0] as IInputListener), ((sManager as SceneManager).SceneGraph[0] as Sam).OnNewInput, ((sManager as SceneManager).SceneGraph[0] as Sam).OnKeyReleased);
                }
            }

            // POPULATE the CollisionManagers collidables List with objects from the Scene Graph:
            cManager.populateCollidables((sManager as SceneManager).SceneGraph);
        }
        /// <summary>
        /// Unloads game content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // DRAW the Entities:
            for(int i = 0; i < (sManager as SceneManager).SceneGraph.Count; i++)
            {
                // DRAW all objects of type 'GameEntity' in the 'entityPool' List.
                spriteBatch.Draw(((sManager as SceneManager).SceneGraph[i] as GameEntity).EntityTexture, ((sManager as SceneManager).SceneGraph[i] as GameEntity).EntityLocn, Color.AntiqueWhite);
                
            }
            spriteBatch.End();

            base.Draw(gameTime);
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
            // CALL the SceneManagers and CollisionManagers update method if the program is running:
            if (running)
            {
                // UPDATE the CollisionManager first:
                cManager.update();
                // THEN update the SceneManager:
                sManager.update();
                // UPDATE the InputManager:
                iManager.update();

                base.Update(gameTime);
            }
        }
    }
}
