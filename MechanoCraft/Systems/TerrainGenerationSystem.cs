using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace MechanoCraft.Systems
{
    internal class TerrainGenerationSystem : IUpdateSystem, IDrawSystem
    {
        TiledMap tiledMap;
        TiledMapRenderer tiledMapRenderer;

        public TerrainGenerationSystem(GraphicsDevice graphics, TiledMap tiledMap)
        {
            this.tiledMap = tiledMap;
            tiledMapRenderer = new TiledMapRenderer(graphics, tiledMap);
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
