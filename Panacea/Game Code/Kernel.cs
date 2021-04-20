using Panacea.Interfaces;
using Panacea.Managers;
using Panacea.UserEventArgs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Panacea.Engine_Code.Camera;
using Panacea.Game_Code;
using Panacea.Game_Code.Game_Entities.Characters;

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

        private const String TILE_MAP_FLOOR_PATH = "Content/Janice_Floor Layer.csv";
        private const String TILE_MAP_COLLISION_PATH = "Content/Janice_Collision Layer.csv";
        private const String TILE_MAP_OBJECTS_PATH = "Content/Janice_Object Layer.csv";

        // DECLARE a GraphicsDeviceManager, call it 'graphics':
        private GraphicsDeviceManager graphics;
        // DECLARE a SpriteBatch, call it 'spriteBatch':
        private SpriteBatch spriteBatch;

        // DECLARE an EntityManager, call it 'eManager'. Store it as its interface IEntityManager:
        private IEntityManager eManager;
        // DECLARE a SceneManager, call it 'sManager'. Store it as its interface ISceneManager:
        private ISceneManager sManager;
        // DECLARE a CollisionManager, call it 'cManager'. Store it as its interface ICollisionManager:
        private ICollisionManager cManager;
        // DECLARE an InputManager, call it 'iManager'. Store it as its interface IInputManager:
        private IInputManager iManager;

        // DECLARE a Camera, call it 'camera':
        private Camera camera;
        // DECLARE a TileMap, call it 'tileMapFloor':
        private TileMap tileMapFloor;
        // DECLARE a TileMap, call it 'tileMapCollisions':
        private TileMap tileMapCollisions;
        // DECLARE a TileMap, call it 'tileMapObjects':
        private TileMap tileMapObjects;

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

            // INITIALIZE the camera:
            camera = new Camera(GraphicsDevice.Viewport);
            // SUBSCRIBE the camera to listen for input events:
            iManager.Subscribe(camera, 
                               camera.OnNewInput,
                               camera.OnKeyReleased,
                               camera.OnNewMouseInput);

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
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // CREATE a new SpriteBatch, which can be used to draw textures:
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // LOADING the game content:
            GameContent.LoadContent(Content);
            // INITALIZE tilemaps:
            tileMapFloor = new TileMap(TILE_MAP_FLOOR_PATH, false);
            tileMapCollisions = new TileMap(TILE_MAP_COLLISION_PATH, true);
            tileMapObjects = new TileMap(TILE_MAP_OBJECTS_PATH, false);
            // SPAWN the game objects:
            this.SpawnObjects();
        }

        /// <summary>
        /// SpawnObjects is used to spawn all game objects into the SceneGraph/EntityPool/CollisionMap. Called in Kernel from LoadContent().
        /// </summary>
        private void SpawnObjects()
        {
            // REQUEST a new 'Sam' object from the EntityManager, and pass it to the SceneManager:
            IEntity sam = eManager.createEntity<Sam>();
            // REQUEST a new 'Mary' object from the EntityManager, and pass it to the SceneManager:
            IEntity mary = eManager.createEntity<Mary>();
            // SPAWN sam into the 
            sManager.spawn(sam);

            // ADD IENTITY TILES TO SCENEGRAPH HERE

            foreach (Tile t in tileMapCollisions.GetTileMap())
            {
                sManager.spawn(t);
            }


            // SET camera focus onto Sam:
            camera.SetFocus(sam as GameEntity);
            

            // ITERATE through the SceneGraph:
            for (int i = 0; i < sManager.SceneGraph.Count; i++)
            {
                if (sManager.SceneGraph[i] is Sam)
                {
                    // SUBSCRIBE to the event that is published in the Sam:
                    (sManager.SceneGraph[i] as Sam).OnEntityTermination += OnEntityTermination;
                    // SUBSCRIBE the paddle to listen for input events and key release events:
                    iManager.Subscribe((sManager.SceneGraph[0] as IInputListener),
                                       (sManager.SceneGraph[0] as Sam).OnNewInput,
                                       (sManager.SceneGraph[0] as Sam).OnKeyReleased,
                                       (sManager.SceneGraph[0] as Sam).OnNewMouseInput);
                }
            }

            // POPULATE the CollisionManagers collidables List with objects from the Scene Graph:
            cManager.PopulateCollidables(sManager.SceneGraph);
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
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(samplerState: SamplerState.PointClamp,
                              transformMatrix: camera.Transform);

            // DRAW the TileMaps:
            tileMapFloor.DrawTileMap(spriteBatch);
            tileMapCollisions.DrawTileMap(spriteBatch);
            tileMapObjects.DrawTileMap(spriteBatch);
            // DRAW the Entities that are in the SceneGraph:
            for (int i = 0; i < sManager.SceneGraph.Count; i++)
            {
                // STOP this loop from drawing the TileMap, as tiles are in the SceneGraph but have their own unique draw method in TileMap:
                if(sManager.SceneGraph[i] is Tile)
                {
                    // IF it's a Tile, break the loop:
                    break;
                }
                // IF not, draw the GameEntity to the SpriteBatch:
                (sManager.SceneGraph[i] as GameEntity).Draw(spriteBatch); 
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
            // CALL the SceneManagers and CollisionManagers Update method if the program is running:
            if (running)
            {
                // UPDATE the CollisionManager first:
                cManager.update();
                // THEN Update the SceneManager:
                sManager.Update(gameTime);
                // UPDATE the InputManager:
                iManager.update();
                // UPDATE the Camera:
                camera.Update(gameTime);

                base.Update(gameTime);
            }
        }
    }
}
