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
    
    }
}
