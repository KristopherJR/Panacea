using COMP2451Project.Interfaces;
using COMP2451Project.Managers;
using COMP2451Project.UserEventArgs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace COMP2451Project
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        #region FIELDS
        // DECLARE a public static Boolean, call it 'running':
        public static Boolean running;
        // DECLARE a reference to a static Texture2D used to store the ball and paddle images. Call them 'ballTexture' and 'paddleTexture' respectively:
        public static Texture2D ballTexture;
        public static Texture2D paddleTexture;

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

            // ASSIGN the 'ballTexture' and 'paddleTexture' with their respected image files:
            ballTexture = Content.Load<Texture2D>("square");
            paddleTexture = Content.Load<Texture2D>("paddle");

            // INITIALIZE the Managers:
            eManager = new EntityManager();
            sManager = new SceneManager();
            cManager = new CollisionManager();
            iManager = new InputManager();

            // REQUEST a new 'Ball' object from the EntityManager, and pass it to the SceneManager:
            sManager.spawn(eManager.createEntity<Ball>());
            // REQUEST two new 'Paddle' objects from the EntityManager, and pass it to the SceneManager:
            sManager.spawn(eManager.createEntity<Paddle>());
            sManager.spawn(eManager.createEntity<Paddle>());

            // CHECK if the SceneGraph at index '1' is a 'Paddle' object:
            if((sManager as SceneManager).SceneGraph[1] is Paddle)
            {
                // SET the location of the Player One paddle to the left of the screen and in the centre:
                ((sManager as SceneManager).SceneGraph[1] as Paddle).EntityLocn = new Vector2(50, (SCREEN_HEIGHT / 2) - (((sManager as SceneManager).SceneGraph[1] as Paddle).EntityTexture.Height) / 2);
                // SUBSCRIBE the paddle to listen for input events and key release events:
                (iManager as InputManager).subscribe(((sManager as SceneManager).SceneGraph[1] as IInputListener), ((sManager as SceneManager).SceneGraph[1] as Paddle).OnNewInput, ((sManager as SceneManager).SceneGraph[1] as Paddle).OnKeyReleased);
            }
            // CHECK if the SceneGraph at index '2' is a 'Paddle' object:
            if ((sManager as SceneManager).SceneGraph[2] is Paddle)
            {
                // SET the location of the Player Two paddle to the right of the screen and in the centre:
                ((sManager as SceneManager).SceneGraph[2] as Paddle).EntityLocn = new Vector2(1500, (SCREEN_HEIGHT/2) - (((sManager as SceneManager).SceneGraph[2] as Paddle).EntityTexture.Height)/2);
                // SUBSCRIBE the paddle to listen for input events and key release events:
                (iManager as InputManager).subscribe(((sManager as SceneManager).SceneGraph[1] as IInputListener), ((sManager as SceneManager).SceneGraph[2] as Paddle).OnNewInput, ((sManager as SceneManager).SceneGraph[2] as Paddle).OnKeyReleased);
            }

            // ITERATE through the SceneGraph:
            for (int i = 0; i < (sManager as SceneManager).SceneGraph.Count; i++)
            {
                // IF the object in the SceneGraph is a 'Ball', call its 'Serve()' method:
                if ((sManager as SceneManager).SceneGraph[i] is Ball)
                { 
                    // SERVE the 'Ball' passing in the screenWidth and screenHeight:
                    ((sManager as SceneManager).SceneGraph[i] as Ball).Serve();
                    // SUBSCRIBE to the event that is published in the Ball:
                    ((sManager as SceneManager).SceneGraph[i] as Ball).OnEntityTermination += OnEntityTermination;
                }
            }

            // POPULATE the CollisionManagers collidables List with objects from the Scene Graph:
            cManager.populateCollidables((sManager as SceneManager).SceneGraph);

            base.Initialize();
            running = true;
        }

        /// <summary>
        /// Event handler for the event OnEntityTermination, fired from the Ball class. This will be triggered each time a Ball goes out of play (touches the right or left wall).
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Details about the event.</param>
        private void OnEntityTermination(object sender, OnEntityTerminationEventArgs eventInformation)
        {
            // UNSUBSCRIBE from the event published by the Ball about to be terminated:
            (sender as Ball).OnEntityTermination -= OnEntityTermination;
            // REMOVE the entity from the Collision Manager:
            cManager.removeCollidable(eventInformation.EntityUName, eventInformation.EntityUID);
            // DESPAWN the entity from the Scene Manager:
            sManager.despawn(eventInformation.EntityUName, eventInformation.EntityUID);
            // DESTROY the entity in the Entity Manager:
            eManager.destroyEntity(eventInformation.EntityUName, eventInformation.EntityUID);

            // CREATE a new 'Ball' object with the EntityManager:
            IEntity newBall = eManager.createEntity<Ball>();
            // SPAWN the entity into the SceneGraph:
            sManager.spawn(newBall);
            // ADD the entity to the CollisionManager:
            cManager.addCollidable((newBall as ICollidable));
            // SUBSCIRBE to the termination event that newBall publishes:
            (newBall as Ball).OnEntityTermination += OnEntityTermination;

            // SERVE the new Ball:
            (newBall as Ball).Serve();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
                // DRAW all objects of type 'PongEntity' in the 'entityPool' List.
                spriteBatch.Draw(((sManager as SceneManager).SceneGraph[i] as PongEntity).EntityTexture, ((sManager as SceneManager).SceneGraph[i] as PongEntity).EntityLocn, Color.AntiqueWhite);
                //Console.WriteLine("My name is: " + sManager.SceneGraph[i].UName + " and my ID is: " + sManager.SceneGraph[i].UID);
            }
            //
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
