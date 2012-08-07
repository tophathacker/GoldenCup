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
        /// Current position of the Projectile
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Constructor for the Projectile class
        /// </summary>
        public Projectile()
        {
            Position = Vector2.Zero;
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
            KeyboardState state = Keyboard.GetState();
            foreach(Keys key in state.GetPressedKeys())
                switch (key)
                {
                    case Keys.Left:
                        // handle left key press
                        Position.X -= 5;
                        break;
                    case Keys.Right:
                        // handle right key press
                        Position.X += 5;
                        break;
                    case Keys.Down:
                        // handle down key press
                        Position.Y += 5;
                        break;
                    case Keys.Up:
                        // handle up key press
                        Position.Y -= 5;
                        break;
                }
        }

        /// <summary>
        /// Draw method for the Projectile Class
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, 100, 100), Color.White);
        }
    }
}
