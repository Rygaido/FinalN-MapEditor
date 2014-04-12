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
namespace mapEditorTest {
    class Button {
        public static int SIZE = 100;

        //image scaling value // image will be scaled to fit static size
       // private float scale;

        private Vector2 loc; //location of tile
        private Texture2D image; //image on tile

        public Vector2 Loc { get { return loc; } }

        public bool hit;
        public bool Hit { get { return hit; } set { hit = value; } }

        public Button(Vector2 l, Texture2D i) {
            Hit = false;
            loc = l;
            image = i;
            //scale = 1;
        }

        //draw tile's icon // requires a started spritebatch parameter
        public void Draw(SpriteBatch sb) {
            Color c = Color.White;
            if (hit) {
                c = Color.DarkGray;
            }
            //draw image at location, on a square of size
            sb.Draw(image, new Rectangle((int)loc.X, (int)loc.Y, SIZE, SIZE), c);
            //sb.Draw(image, loc, null, c, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
        }
        //overload draw method, draw text on screen
        public void Draw(SpriteBatch sb, string s) {
            Color c = Color.White;
            if (hit) {
                c = Color.DarkGray;
            }
            //draw image at location, on a square of size
            sb.Draw(image, new Rectangle((int)loc.X, (int)loc.Y, SIZE, SIZE), c);
            sb.DrawString(ImageBank.font,s, new Vector2((int)loc.X, (int)loc.Y), Color.Black);
            //sb.Draw(image, loc, null, c, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
        }

        //returns true if mouse is over button
        public bool MouseOver(MouseState current) {
            return (current.X > Loc.X && current.Y > Loc.Y &&
                current.X < Loc.X + Button.SIZE && current.Y < Loc.Y + Button.SIZE);
            
        }
    }
}
