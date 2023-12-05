using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace MechanoCraft.Generator
{
    internal class TerrainGenerator : IUpdateSystem, IDrawSystem
    {
        TiledMap tiledMap;
        TiledMapRenderer tiledMapRenderer;

        public TerrainGenerator(GraphicsDevice graphics, TiledMap tiledMap) 
        {
            this.tiledMap = tiledMap;
            tiledMapRenderer = new TiledMapRenderer(graphics,tiledMap);
        }

        public void Dispose()
        {

        }

        public void Draw(GameTime gameTime)
        {
            tiledMapRenderer.Draw();
        }

        public void Initialize(World world)
        {

        }

        public void Update(GameTime gameTime)
        {
            tiledMapRenderer.Update(gameTime);
        }
    }
}
