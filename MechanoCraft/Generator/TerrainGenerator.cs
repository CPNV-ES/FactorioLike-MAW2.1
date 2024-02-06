using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
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
        OrthographicCamera camera;

        public TerrainGenerator(GraphicsDevice graphics, TiledMap tiledMap, OrthographicCamera camera) 
        {
            this.tiledMap = tiledMap;
            tiledMapRenderer = new TiledMapRenderer(graphics,tiledMap);
            this.camera = camera;
        }

        public void Dispose()
        {

        }

        public void Draw(GameTime gameTime)
        {
            tiledMapRenderer.Draw(camera.GetViewMatrix());
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
