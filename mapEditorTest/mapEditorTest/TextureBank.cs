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

//images are stored in lists, new values can be added at will by adding an image to content, 
//and loading it into a list in the load method of Game1. It should be easy to add New objects at any time
//The decryption method on the Actual game will need to be updated everytime lists are changed, but that should be simple
//Empty is kept apart, it is an exception and technically the most important value


////BE CAREFUL - the order of lists is important - theoretically new objects can be added to the end of the lists at anytime,
//but adding to the begining of a list would change values of old objects, affecting all previously built maps 

namespace mapEditorTest {
    static class TextureBank {
        
        public static Texture2D empty; //nothing occupying space

        public static List<Texture2D> platforms = new List<Texture2D>(); //list of stand-on-able objects (first is wall/floor)
        public static List<Texture2D> enemies = new List<Texture2D>();//list of enemies
        public static List<Texture2D> obstacles = new List<Texture2D>();//list of obstacles
        public static List<Texture2D> extra = new List<Texture2D>(); //list of other things such as spawn points and end points

        //list containing lists so that we can loop through lists
        public static List<List<Texture2D>> lists = new List<List<Texture2D>>() { platforms, enemies,obstacles,extra}; //we're in too deep

        //theory: spcae char represents blank space, 
        //char(space+1) to char(space+listSize) represent platforms
        //the next (listSize) of chars represent enemies
        //repeat for all lists, this way all chars can be identified by subtracting (space) and modulus(listSize) to get index,  
        //list can be determined by subtracting (space) and dividing by listSize
        public static int firstChar = (int)' '; //lets start at space for empty object
        public static int listSize = 50; //and make list size a large number so that we can always expand later
    }
}
