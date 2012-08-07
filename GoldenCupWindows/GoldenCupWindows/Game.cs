/****************************************************************************
 * Contributors: Ryan Hatfield
 * 
 * Put other comments here..
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GoldenCupWindows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Projectile> projectiles = new List<Projectile>();
        List<Block> blocks = new List<Block>();
        Texture2D blockTexture;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Projectile mrProjectile = new Projectile();
            projectiles.Add(mrProjectile);


            base.Initialize();
        }

        private void buildBlocks()
        {
            for (int i = 0; i < 500; i += 50)
            {
                for (int j = 0; j < 1000; j += 50)
                {
                    //make the new block
                    Block newBlock = new Block();
                    newBlock.BoundingBox.X = j;
                    newBlock.BoundingBox.Y = i;
                    newBlock.Texture = blockTexture;

                    //now make sure the block isn't going to intersect a previous block
                    bool intersects = false;
                    foreach (Block block in blocks)
                        if (newBlock.BoundingBox.Intersects(block.BoundingBox))
                            intersects = true;
                    if(!intersects)
                        blocks.Add(newBlock);
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (Projectile projectile in projectiles)
                projectile.Texture = Content.Load<Texture2D>("rock");
            blockTexture = Content.Load<Texture2D>("cobblestone");
            buildBlocks();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                buildBlocks();

            //update all projectiles
            foreach (Projectile projectile in projectiles)
                projectile.Update(gameTime);
            //update all blocks
            foreach (Block block in blocks)
                block.Update(gameTime);

            //now do some basic collision detection between the projectiles and the blocks

            //list of blocks that intersect with a projectile
            List<Block> blocksToRemove = new List<Block>();

            foreach (Projectile projectile in projectiles)
                foreach (Block block in blocks)
                    if(projectile.BoundingBox.Intersects(block.BoundingBox))
                        blocksToRemove.Add(block);

            //can't just remove the blocks from the foreach loop, 
            //have to wait until after .. so remove them now
            foreach (Block block in blocksToRemove)
            {
                blocks.Remove(block);
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //draw all projectiles
            foreach (Projectile projectile in projectiles)
                projectile.Draw(gameTime, spriteBatch);
            
            //draw all blocks
            foreach (Block block in blocks)
                block.Draw(gameTime,spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
