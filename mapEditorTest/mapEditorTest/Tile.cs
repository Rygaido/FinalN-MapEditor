#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

//each tile represents a char in the matrix
//each char corresponds to an image on the tile
//clicking the tile will allow for manipulation of char value
//Tiles scale any image to fit, change SIZE variable if grid is too large or small to be functional
namespace mapEditorTest{
    class Tile{
        //the size of all tiles (tiles are square, width = height = size)
        //THIS VALUE CONTROLS THE SCALE OF ENTIRE GRID
        public static int SIZE = 50;

        //image scaling value // image will be scaled to fit static size
        private float scale;

        private Vector2 loc; //location of tile
        private Texture2D image; //image on tile
        private char value = ' '; //txt value corresponding to image

        //public static int HEIGHT = 50; //seperate height and width values removed, tiles are square
        //public static int WIDTH = 50;
        //private int scaleH; // height and width scale values removed, tiles are square
        //private int scaleW;

        //get and set property for Vector2 loc
        public Vector2 Loc {
            get { return loc; }
            set { loc = value; }
        }

        //tile constructor
        public Tile() {
            scale = 1.0f; //scale set to 1 by default
            //Update();
        }

        //open form for selection of object
        public void leftClick() {
            value = (char)((int)value + 1);

            
            //Update();
        }

        //shortcut making empty object
        public void rightClick() {
            value = ' ';

            //call update after all changes to value
            //Update();
        }

        //check char value and update image and scale
        private void Update() {

            int num = (int)value - (int)' '; //convert value to an int, and subtract by space (empty value)
            int listNum = num / TextureBank.listSize; //get the quotient (list the image is located in)
            num %= TextureBank.listSize; //get the remainder (index in said list)

            //listSize is larger than actual list, check to make sure num is within actual list
            if (num >= TextureBank.lists[listNum].Count) {
                value = (char)((int)value + TextureBank.listSize-num); //set value to first in next list
                listNum++; //move to fist index of next list if exceeding previous
                num = 0;

                if (listNum >= TextureBank.lists.Count) { //exceeding last list, reset to space
                    value = ' ';
                }
            }

            if (value == ' ') { //space is empty, special case
                image = TextureBank.empty;
            }
            else { //otherwise char is somewhere within lists
                image = TextureBank.lists[listNum][num]; //get the specified image from the specified list
            }

            scale = SIZE / ((float)image.Height);  //adjuct scale of image
        }

        //draw tile's icon // requires a started spritebatch parameter
        public void Draw(SpriteBatch sb) {
            Update();
            
            //draw scaled image at location, the rest of the values here are placeholders
            sb.Draw(image,loc,null,Color.White,0.0f,new Vector2(0,0),scale,SpriteEffects.None,0.0f);
            /*if (value == (char)0) {
                
                sb.Draw(TextureBank.empty, loc, Color.White);
            }*/
            
        }
    }
}
