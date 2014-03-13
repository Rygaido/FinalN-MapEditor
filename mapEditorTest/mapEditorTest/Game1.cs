#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
#endregion

namespace mapEditorTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Tile[,] grid;
        int width = 5; //10 rows and columns by default
        int height = 5;

        MouseState current;
        MouseState previous;

       // Texture2D tex;

        //scrapped form
        //GridControl g; //interface for resizing grid, saving maps and loading maps

        public Game1(): base(){
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize(){
            // TODO: Add your initialization logic here
            int defaultSize = 10;
            grid = new Tile[defaultSize,defaultSize];

            newGrid(height, width); // initialize the grid

            

            /* scrapped form-threaded control
            g = new GridControl();
            Thread t = new Thread(ControlWindow);
            t.Start(); //*/

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent(){
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //empty sprite
            TextureBank.empty = Content.Load<Texture2D>("noBlock");

            //add all platforming sprites, in order of selection
            TextureBank.platforms.Add(Content.Load<Texture2D>("wall"));
            TextureBank.platforms.Add(Content.Load<Texture2D>("platform"));
            

            //add all enemy sprite to a list (in order please)
            TextureBank.enemies.Add(Content.Load<Texture2D>("enemy"));

            //add all obstacle sprites
            TextureBank.obstacles.Add(Content.Load<Texture2D>("obstacle"));

            //add all extra sprites
            TextureBank.extra.Add(Content.Load<Texture2D>("startPt"));
            TextureBank.extra.Add(Content.Load<Texture2D>("endPt"));
            TextureBank.extra.Add(Content.Load<Texture2D>("invisBblock"));


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent(){
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previous = current; //update mouse states
            current = Mouse.GetState();

            //get mouse coordinates on grid
            int x = current.X;
            int y = current.Y;
            x = x / Tile.SIZE;
            y = y / Tile.SIZE;

            //check if mouse is over grid
            if (x >= 0 && x < width && y >= 0 && y < height) {
                
                //check if left button was clicked, but wasn't clicked earlier 
                if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed) {

                    grid[x, y].leftClick();
                }
                //check if right button was clicked, but wasn't clicked earlier 
                if (current.RightButton == ButtonState.Pressed && previous.RightButton != ButtonState.Pressed) {

                    grid[x, y].rightClick();
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //call draw method on all tiles, loop through rows and columns
            for (int i = 0; i < height ; i++) {
                for (int j = 0; j < width ; j++) {
                    grid[i, j].Draw(spriteBatch);
                }
            }
            //spriteBatch.Draw(tex, grid[0, 0].Loc, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //New Grid creation method
        public void newGrid(int r, int c) {
            //create grid of size
            //loop through all rows and columns
            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c; j++) {
                    //create new tiles and place rectangles according to location in matrix
                    grid[i, j] = new Tile();
                    grid[i, j].Loc = new Vector2(i*Tile.SIZE,j*Tile.SIZE); //location is row and column * tile size
                }
            }

        }

        /* Scrapped windows form interface
         * Monogame doesn't share 
        //method to open form, run simultaneous with game screen
        protected void ControlWindow() {
            g.Show();

            Thread.Sleep(99999);
        } */
    }
}
