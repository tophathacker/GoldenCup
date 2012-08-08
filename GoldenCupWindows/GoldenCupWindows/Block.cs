using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GoldenCupWindows
{
    class Block
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
        public Block()
        {
            BoundingBox = new Rectangle(0, 0, 10, 10);
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
