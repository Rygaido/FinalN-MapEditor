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
        public static int SIZE = 50;

        //image scaling value // image will be scaled to fit static size
        private float scale;

        private Vector2 loc; //location of tile
        private Texture2D image; //image on tile

        public Vector2 Loc { get { return loc; } }

        public Button(Vector2 l, Texture2D i) {

            loc = l;
            image = i;
            scale = 1;
        }

        //draw tile's icon // requires a started spritebatch parameter
        public void Draw(SpriteBatch sb) {

            //draw scaled image at location, the rest of the values here are placeholders
            sb.Draw(image, loc, null, Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 0.0f);
        }
    }
}
