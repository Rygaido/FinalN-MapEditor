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
using System.IO;
#endregion

namespace mapEditorTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game{
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //matrix of tiles
        Tile[,] grid;
        int gridX=50;
        int gridY=50;

        //total width and height of grid
        int width = 24; 
        int height = 18;

        //x & y coords of visable segment of grid
        int scrollX=0;
        int scrollY=0;

        //max length and width of visable segment of grid
        int viewX = 12;
        int viewY = 9;

        //mousestates// used for clicking buttons and tiles
        MouseState current;
        MouseState previous;

        //keyboardstates// used for scrolling
        KeyboardState currentK;
        KeyboardState previousK;

        //buttons used for
        Button save;
        Button load;

        //add and removal buttons for resizing grid
        Button addColL;
        Button addColR;
        Button addRowT;
        Button addRowB;

        bool locked = false; //used to prevent multiple accesses to dat file
        bool cntrl = false;
        bool shift = false;
        bool saving = false;
        bool loading = false;
        string saveString = "";
       // Texture2D tex;

        //scrapped form
        //GridControl g; //interface for resizing grid, saving maps and loading maps

        public Game1(): base(){
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
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
            //int defaultSize = 10;
            grid = new Tile[height,width];

            newGrid(height, width); // initialize the grid

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
            ImageBank.empty = Content.Load<Texture2D>("noBlock");

            //add all platforming sprites, in order of selection
            ImageBank.platforms.Add(Content.Load<Texture2D>("wall"));
            ImageBank.platforms.Add(Content.Load<Texture2D>("platform"));
            
            //add all enemy sprite to a list (in order please)
            ImageBank.enemies.Add(Content.Load<Texture2D>("walkingMinion"));
            ImageBank.enemies.Add(Content.Load<Texture2D>("enemy2idle"));

            //add all obstacle sprites
            ImageBank.obstacles.Add(Content.Load<Texture2D>("obstacle"));

            //add all extra sprites
            ImageBank.extra.Add(Content.Load<Texture2D>("startPt"));
            ImageBank.extra.Add(Content.Load<Texture2D>("endPt"));
            ImageBank.extra.Add(Content.Load<Texture2D>("invisBblock"));

            //spritefonts
            ImageBank.font = Content.Load<SpriteFont>("Font1");

            //button sprites
            ImageBank.saveBtn = Content.Load<Texture2D>("saveBtn");
            ImageBank.loadBtn = Content.Load<Texture2D>("loadBtn");


            save = new Button(new Vector2(700, 400), ImageBank.saveBtn);
            load = new Button(new Vector2(700, 500), ImageBank.loadBtn);

            addColR = new Button(new Vector2(700, 000), ImageBank.empty);
            addRowB = new Button(new Vector2(700, 100), ImageBank.empty);
            addColL = new Button(new Vector2(700, 200), ImageBank.empty);
            addRowT = new Button(new Vector2(700, 300), ImageBank.empty);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent(){
            // TODO: Unload any non ContentManager content here
        }//

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

            //scrolling with keyboard
            previousK = currentK;
            currentK = Keyboard.GetState();

            if (saving || loading) {

                foreach (Keys k in currentK.GetPressedKeys()) {
                    string keys = k.ToString();

                    if (!previousK.IsKeyDown(k)) {
                        if (keys.Length == 1) {
                            if (Char.IsLetterOrDigit(keys[0])) {
                                if (!shift) {
                                    saveString += keys[0].ToString().ToLower();
                                }
                                else {
                                    saveString += keys[0];
                                }
                            }

                        }
                        if (k.Equals(Keys.Back) && saveString.Length > 0) {
                            saveString = saveString.Substring(0, saveString.Length - 1);
                        }
                        if (k.Equals(Keys.OemMinus) && shift) {
                            saveString += "_";
                        }
                    }
                }
            }

            else {
                //get mouse coordinates on grid
                int x = current.X - gridX;
                int y = current.Y - gridY;
                x = x / Tile.SIZE;
                y = y / Tile.SIZE;

                //check if mouse is over grid
                if (x >= 0 && x < viewX && y >= 0 && y < viewY) {

                    //adjust coords for scrolling before actions
                    x += scrollX;
                    y += scrollY;

                    //check if left button was clicked, but wasn't clicked earlier 
                    if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed) {

                        grid[y, x].leftClick();
                        ChangesMade();
                    }
                    //check if right button was clicked, but wasn't clicked earlier 
                    if (current.RightButton == ButtonState.Pressed && previous.RightButton != ButtonState.Pressed) {

                        grid[y, x].rightClick();
                        ChangesMade();
                    }
                }
                //save button clicked
                if (save.MouseOver(current) && !locked) {
                    if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed && !save.Hit) {
                        save.Hit = true;
                        saving = true;
                    }
                }
                //load button clicked
                if (load.MouseOver(current) && !locked) {
                    if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed && !load.Hit) {
                        load.Hit = true;
                        loading = true;
                    }
                }

                int speed = 1;

                //speed up if control key is held
                if (currentK.IsKeyDown(Keys.RightControl) || currentK.IsKeyDown(Keys.LeftControl)) {
                    speed = 5;
                    cntrl = true;
                }
                else {
                    cntrl = false;
                }
                

                //right key was pressed
                if (currentK.IsKeyDown(Keys.Right) && !previousK.IsKeyDown(Keys.Right)) {
                    if (scrollX + speed <= width - viewX) {//check if room to scroll
                        scrollX += speed;
                        ScrollGrid(-Tile.SIZE * speed, 0);
                    }
                }
                //left key was pressed
                if (currentK.IsKeyDown(Keys.Left) && !previousK.IsKeyDown(Keys.Left)) {
                    if (scrollX - speed >= 0) {//check if room to scroll
                        scrollX -= speed;
                        ScrollGrid(+Tile.SIZE * speed, 0);
                    }
                }

                //down key was pressed
                if (currentK.IsKeyDown(Keys.Down) && !previousK.IsKeyDown(Keys.Down)) {
                    if (scrollY + speed <= height - viewY) {//check if room to scroll
                        scrollY += speed;
                        ScrollGrid(0, -Tile.SIZE * speed);
                    }
                }
                //up key was pressed
                if (currentK.IsKeyDown(Keys.Up) && !previousK.IsKeyDown(Keys.Up)) {

                    if (scrollY - speed >= 0) {//check if room to scroll
                        scrollY -= speed;
                        ScrollGrid(0, +Tile.SIZE * speed);
                    }
                }

               
                //add left column button clicked
                if (addColL.MouseOver(current)) {
                    if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed) {
                        if (!shift) {
                            AddCol(speed, true);
                        }
                        else {
                            AddCol(-speed, true);
                        }
                    }
                }//add right column button clicked
                if (addColR.MouseOver(current)) {
                    if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed) {
                        if (!shift) {
                            AddCol(speed, false);
                        }
                        else {
                            AddCol(-speed, false);
                        }
                    }
                }//add bottom row button clicked
                if (addRowB.MouseOver(current)) {
                    if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed) {
                        if (!shift) {
                            AddRow(speed, false);
                        }
                        else {
                            AddRow(-speed, false);
                        }
                    }
                }//add bottom row button clicked
                if (addRowT.MouseOver(current)) {
                    if (current.LeftButton == ButtonState.Pressed && previous.LeftButton != ButtonState.Pressed) {
                        if (!shift) {
                            AddRow(speed, true);
                        }
                        else {
                            AddRow(-speed, true);
                        }
                    }
                }
            }
            //enter key was pressed
            if (currentK.IsKeyDown(Keys.Enter) && !previousK.IsKeyDown(Keys.Enter)) {

                if (saveString.ToLower() == "cancel") {
                        saveString = "";
                        loading = false;
                }

                if (saving) {
                    SaveGrid();
                }
                if (loading) {
                   LoadGrid();
                }
            }
            //invert row/col buttons when shift
            if (currentK.IsKeyDown(Keys.RightShift) || currentK.IsKeyDown(Keys.LeftShift)) {
                shift = true;
            }
            else {
                shift = false;
            }

            base.Update(gameTime);
        }
        //move all tiles in grid by specified amounts to stay onscreen
        //pass in 0 to not change a dimension
        private void ScrollGrid(int x, int y) {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    grid[i, j].Loc = new Vector2(grid[i, j].Loc.X + x,grid[i, j].Loc.Y+y);
                }
            }
        }

        //reset save and load buttons because changes have been made to grid
        private void ChangesMade(){

            save.Hit = false;
            load.Hit = false; 
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime){
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //call draw method on all tiles, loop through rows and columns
            for (int i = scrollY; i < Math.Min(scrollY+ viewY, height); i++) {
                for (int j = scrollX; j < Math.Min(scrollX+viewX,width); j++) {
                    grid[i, j].Draw(spriteBatch);
                }
            }
            string s = "";
            //draw scroll values
            spriteBatch.DrawString(ImageBank.font,""+scrollX,new Vector2(gridX,gridY/2),Color.Black);
            spriteBatch.DrawString(ImageBank.font,""+scrollY,new Vector2(gridX/4,gridY),Color.Black);
            //draw far-end grid values
            spriteBatch.DrawString(ImageBank.font,(scrollX+viewX)+"   /"+width, new Vector2((gridX+Tile.SIZE*(viewX-1)), gridY/2), Color.Black);
            spriteBatch.DrawString(ImageBank.font,(scrollY+viewY)+"\n/"+height, new Vector2(gridX/4,(gridY+Tile.SIZE*(viewY-1))), Color.Black);
            spriteBatch.DrawString(ImageBank.font, "Use arrow keys to scroll\nHold Ctrl to scroll 5x faster", new Vector2(gridX / 4, (gridY + Tile.SIZE * (viewY))), Color.Black);
            spriteBatch.DrawString(ImageBank.font, "Hold shfit to remove rows or columns\nHold Ctrl to add/rem rows/cols 5x faster", new Vector2(gridX * 6, (gridY + Tile.SIZE * (viewY))), Color.Black);

            int xx= 100;
            if (saving || loading) {
                spriteBatch.Draw(ImageBank.empty, new Rectangle(xx, xx, 500, 200), Color.Cyan);

                s = "save";
                if (loading) { s = "load"; }
                spriteBatch.DrawString(ImageBank.font, "Enter name of "+s+" file (or type cancel):\n" + saveString, new Vector2(xx+10, xx+10), Color.Black);
            }
            save.Draw(spriteBatch);
            load.Draw(spriteBatch);
            //spriteBatch.Draw(tex, grid[0, 0].Loc, Color.White);

            s = "Add";
            if (shift) { s = "Remove"; }
            addColL.Draw(spriteBatch, s+" \ncolumn\nLeft");
            addRowB.Draw(spriteBatch, s+" \nrow\nBottom");
            addColR.Draw(spriteBatch, s + " \ncolumn\nRight");
            addRowT.Draw(spriteBatch, s + " \nrow\nTop");
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //New Grid creation method
        public void newGrid(int r, int c) {
            //create grid of size
            grid = new Tile[r, c];

            scrollX = 0;
            scrollY = 0;
            //loop through all rows and columns
            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c; j++) {
                    //create new tiles and place rectangles according to location in matrix
                    grid[i, j] = NewTile(i, j);
                }
            }
            width = c;
            height = r;
        }

        //write grid's info to a .dat file
        public void SaveGrid() {
            locked = true;

            string s = ""; //convert grid of chars to a single string
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width;j++ ) {
                    s += grid[i, j].Value;
                }
            }
            //Open new binary writer //for testing phase, there is no option to rename file
            BinaryWriter writer= new BinaryWriter(File.Open(saveString+".dat",FileMode.Create));

            //write number of rows, then columns then the string
            writer.Write(height);
            writer.Write(width);
            writer.Write(s);

            writer.Close();
            locked = false;
            saving = false;
            saveString = "";
            //save.Reset();
        }

        public void LoadGrid() {
            locked = true;
            BinaryReader reader = null;
            try {
                //Open new binary reader //for testing phase, there is no option to rename file
                reader = new BinaryReader(File.Open(saveString + ".dat", FileMode.Open));
            }
            catch (Exception e) {
                saveString = "Invalid_File_Name";
                return;
            }

            //write number of rows, then columns then the string
            int r = reader.ReadInt32();
            int c = reader.ReadInt32();
            string s = reader.ReadString();
            char[] chars = s.ToCharArray();

            newGrid(r,c);

            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c; j++) {
                    grid[i, j].Value = chars[(i*c+j)];
                }
            }

            reader.Close();
            locked = false;
            saveString = "";
            loading = false;
            //load.Reset();
        }

        //adds specified number of columns, a negative num removes cols
        //invert parameter removes it from smaller-index side
        public void AddCol(int num, bool invert) {
            if (width + num <= 0) { return; } //cancel if going below 0

            Tile[,] g = new Tile[height, width + num];
            if (invert) {
                scrollX+=num;

                if (num < 0 && Math.Abs(num) > scrollX) {
                    scrollX -= num;
                    ScrollGrid(+Tile.SIZE * num,0);
                }
            }
            for (int i = 0; i < height; i++) { //fill new grid 
                for (int j = 0; j < width + num; j++) {
                    if (j < width && !invert) {
                        g[i, j] = grid[i, j];
                    } else if(j>=num && invert){
                        g[i, j] = grid[i, j - num];
                    }else {
                        g[i, j] = NewTile(i, j);
                    }
                }
            }
            //replace grid with new grid
            grid = g;
            width += num;
        }

        //adds specified number of rows, a negative num removes rows
        //invert parameter removes it from smaller-index side
        public void AddRow(int num,bool invert) {
            if (height + num <= 0) { return; } //cancel if going below 0

            Tile[,] g = new Tile[height+num, width];
            if (invert) {
                scrollY+=num;
                if (num < 0 && Math.Abs(num) > scrollY) {
                    scrollY -= num;
                    ScrollGrid(0, +Tile.SIZE * num);
                }
            }
            for (int i = 0; i < height+num; i++) { //fill new grid 
                for (int j = 0; j < width; j++) {
                    if (i < height && !invert) {
                        g[i, j] = grid[i, j];
                    } else if (i >= num && invert) {
                        g[i, j] = grid[i-num, j];
                    } else {
                        g[i, j] = NewTile(i,j);
                    }
                }
            }
            //replace grid with new grid
            grid = g;
            height += num;
        }

        //makes a new tile in specified location
        private Tile NewTile(int i, int j) {
            Tile t =new Tile();
            //location is scroll added to row and column * tile size add the initial grid coordinates
            t.Loc = new Vector2((-scrollX+j) * Tile.SIZE + gridX, 
                (-scrollY+i) * Tile.SIZE + gridY); 
            return t;
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
