using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panacea.Engine_Code.Camera;
using Panacea.Engine_Code.Interfaces;
using Panacea.Engine_Code.Managers;
using Panacea.Game_Code;
using Panacea.Game_Code.Game_Entities.Characters;
using Panacea.Interfaces;
using Panacea.Managers;
using Panacea.UserEventArgs;
using System;

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
        // DECLARE a const String, call it TILE_MAP_FLOOR_PATH. Set it to the File Path of the floor layer: 
        private const String TILE_MAP_FLOOR_PATH = "Content/Janice_Floor Layer.csv";
        // DECLARE a const String, call it TILE_MAP_COLLISION_PATH. Set it to the File Path of the collision layer:
        private const String TILE_MAP_COLLISION_PATH = "Content/Janice_Collision Layer.csv";
        // DECLARE a const String, call it TILE_MAP_OBJECTS_PATH. Set it to the File Path of the object layer:
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
        // DECLARE an NavigationManager, call it 'nManager'. Store it as its interface INavigationManager:
        private INavigationManager nManager;

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
            nManager = new NavigationManager();

            // INITIALIZE the camera:
            camera = new Camera(GraphicsDevice.Viewport);
            // SUBSCRIBE the camera to listen for input events:
            iManager.Subscribe(camera,
                               camera.OnNewInput,
                               camera.OnKeyReleased,
                               camera.OnNewMouseInput);

            // INITIALISE base class:
            base.Initialize();
            // SET running to true:
            running = true;
        }

        /// <summary>
        /// Event handler for the event OnEntityTermination, fired from the Player class. This will be triggered each time a Player goes out of play (touches the right or left wall).
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Details about the event.</param>
        private void OnEntityTermination(object sender, OnEntityTerminationEventArgs eventInformation)
        {
            // UNSUBSCRIBE from the event published by the Player about to be terminated:
            (sender as Player).OnEntityTermination -= OnEntityTermination;
            // REMOVE the entity from the Collision Manager:
            cManager.removeCollidable(eventInformation.EntityUName, eventInformation.EntityUID);
            // DESPAWN the entity from the Scene Manager:
            sManager.despawn(eventInformation.EntityUName, eventInformation.EntityUID);
            // DESTROY the entity in the Entity Manager:
            eManager.destroyEntity(eventInformation.EntityUName, eventInformation.EntityUID);

            // CREATE a new 'Player' object with the EntityManager:
            IEntity newPlayer = eManager.createEntity<Player>();
            // SPAWN the entity into the SceneGraph:
            sManager.spawn(newPlayer);
            // ADD the entity to the CollisionManager:
            cManager.addCollidable((newPlayer as ICollidable));
            // SUBSCIRBE to the termination event that newBall publishes:
            (newPlayer as Player).OnEntityTermination += OnEntityTermination;
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
            // REQUEST a new 'Player' object from the EntityManager, and pass it to the SceneManager. Call it Sam.:
            IEntity sam = eManager.createEntity<Player>();
            // REQUEST a new 'NPC' object from the EntityManager, and pass it to the SceneManager. Call it Mary.:
            IEntity mary = eManager.createEntity<NPC>();
            // SPAWN sam into the SceneGraph:
            sManager.spawn(sam);
            // SPAWN mary into the SceneGraph:
            sManager.spawn(mary);

            // ADD IENTITY TILES TO SCENEGRAPH HERE

            // FOREACH Tile in TileMap collision Layer:
            foreach (Tile t in tileMapCollisions.GetTileMap())
            {
                // SPAWN the Tiles into the SceneGraph:
                sManager.spawn(t);
            }

            // SET camera focus onto Player:
            camera.SetFocus(sam as GameEntity);

            // ITERATE through the SceneGraph:
            for (int i = 0; i < sManager.SceneGraph.Count; i++)
            {
                if (sManager.SceneGraph[i] is Player)
                {
                    // SUBSCRIBE to the event that is published in the Player:
                    (sManager.SceneGraph[i] as Player).OnEntityTermination += OnEntityTermination;
                    // SUBSCRIBE the paddle to listen for input events and key release events:
                    iManager.Subscribe((sManager.SceneGraph[0] as IInputListener),
                                       (sManager.SceneGraph[0] as Player).OnNewInput,
                                       (sManager.SceneGraph[0] as Player).OnKeyReleased,
                                       (sManager.SceneGraph[0] as Player).OnNewMouseInput);
                }
            }

            // POPULATE the CollisionManagers collidables List with objects from the Scene Graph:
            cManager.PopulateCollidables(sManager.SceneGraph);
            // PASS a reference to the collision tile map to the NavigationManager:
            nManager.NavigationGrid = tileMapCollisions;
            nManager.AddPathFinder(mary as IPathFinder);
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
                if (sManager.SceneGraph[i] is Tile)
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
                // UPDATE the NavigationManager:
                nManager.Update(gameTime);
                // THEN Update the SceneManager:
                sManager.Update(gameTime);
                // UPDATE the InputManager:
                iManager.update();

                // UPDATE the Camera:
                camera.Update(gameTime);
                // UPDATE base class:
                base.Update(gameTime);
            }
        }
    }
}
