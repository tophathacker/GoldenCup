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
        Texture2D LevelTexture;
        Camera2d camera;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            camera = new Camera2d();
            //camera.Pos = new Vector2(500.0f, 200.0f);
            // camera.Zoom = 2.0f // Example of Zoom in
            // camera.Zoom = 0.5f // Example of Zoom out
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

            graphics.PreferMultiSampling = false;

            base.Initialize();
        }

        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        private void buildBlocks()
        {
            /*
            for (int i = 0; i < GraphicsDevice.Viewport.Height; i += 50)
            {
                for (int j = 0; j < GraphicsDevice.Viewport.Width; j += 50)
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
             */

            Color[,] levelData = TextureTo2DArray(LevelTexture);
            for (int i = 0; i < LevelTexture.Width; i++)
            {
                for (int j = 0; j < LevelTexture.Height; j++)
                {
                    if (levelData[i, j] == Color.Black)
                    {
                        //make the new block
                        Block newBlock = new Block();
                        newBlock.BoundingBox.X = i*10;
                        newBlock.BoundingBox.Y = j*10;
                        newBlock.Texture = blockTexture;

                        //now make sure the block isn't going to intersect a previous block
                        bool intersects = false;
                        foreach (Block block in blocks)
                            if (newBlock.BoundingBox.Intersects(block.BoundingBox))
                                intersects = true;
                        if (!intersects)
                            blocks.Add(newBlock);
                    }
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
            LevelTexture = Content.Load<Texture2D>("LevelDesign");
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

            KeyboardState state = Keyboard.GetState();
            foreach (Keys key in state.GetPressedKeys())
                switch (key)
                {
                    case Keys.Q:
                        // handle left key press
                        camera.Zoom -= .01f;
                        break;
                    case Keys.E:
                        camera.Zoom += .01f;
                        break;
                    case Keys.A:
                        camera.Pos = new Vector2(camera.Pos.X - 1, camera.Pos.Y);
                        break;
                    case Keys.D:
                        camera.Pos = new Vector2(camera.Pos.X + 1, camera.Pos.Y);
                        break;
                    case Keys.W:
                        camera.Pos = new Vector2(camera.Pos.X, camera.Pos.Y - 1);
                        break;
                    case Keys.S:
                        camera.Pos = new Vector2(camera.Pos.X, camera.Pos.Y + 1);
                        break;
                    case Keys.F10:
                        graphics.ToggleFullScreen();
                        break;
                }

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

            foreach (Projectile projectile in projectiles)
            {
                projectile.BoundingBox.X = (int)camera.get_mouse_vpos().X;
                projectile.BoundingBox.Y = (int)camera.get_mouse_vpos().Y;
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

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        camera.get_transformation(GraphicsDevice));

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
