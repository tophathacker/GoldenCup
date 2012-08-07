using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoldenCupWindows
{
    /// <summary>
    /// A class to hold all types of projectiles in the game
    /// </summary>
    class Projectile
    {
        /// <summary>
        /// Graphic for the Projectile
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Bounding box that tells the size and position of the Projectile
        /// </summary>
        public Rectangle BoundingBox;

        /// <summary>
        /// Constructor for the Projectile class
        /// </summary>
        public Projectile()
        {
            BoundingBox = new Rectangle(0, 0, 100, 100);
        }

        /// <summary>
        /// Initialize method for the Projectile class
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// LoadContent method for the Projectile Class
        /// </summary>
        public void LoadContent()
        {
        }

        /// <summary>
        /// Update method for the Projectile Class
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            /*
            // this is for using the keyboard to move the projectile around. only for testing.
            KeyboardState state = Keyboard.GetState();
            foreach(Keys key in state.GetPressedKeys())
                switch (key)
                {
                    case Keys.Left:
                        // handle left key press
                        BoundingBox.X -= 5;
                        break;
                    case Keys.Right:
                        // handle right key press
                        BoundingBox.X += 5;
                        break;
                    case Keys.Down:
                        // handle down key press
                        BoundingBox.Y += 5;
                        break;
                    case Keys.Up:
                        // handle up key press
                        BoundingBox.Y -= 5;
                        break;
                }
            */

            // this is for using the mouse to move the projectile around. only for testing.
            MouseState mouseState = Mouse.GetState();
            BoundingBox.X = mouseState.X;
            BoundingBox.Y = mouseState.Y;
        }

        /// <summary>
        /// Draw method for the Projectile Class
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            if (Texture != null && BoundingBox != null)
                spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }
    }
}
