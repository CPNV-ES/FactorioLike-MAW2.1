using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MechanoCraft.Crafting.Recipes;
using MechanoCraft.Inventory;
using MonoGame.Extended.Tiled;
using MechanoCraft.Entities.Player;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using MechanoCraft.Systems;

namespace MechanoCraft
{
    public class MechanoCraft : Game
    {
        private GraphicsDeviceManager _graphics;
        private OrthographicCamera _camera;
        private SpriteBatch _spriteBatch;
        private World _world;

        public MechanoCraft()
        {
            _graphics = new GraphicsDeviceManager(this);     
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, _graphics.GraphicsDevice, _graphics.GraphicsDevice.ScissorRectangle.Width, _graphics.GraphicsDevice.ScissorRectangle.Height);
            _camera = new OrthographicCamera(viewportAdapter);

            ItemCreator.CreateItems();
            Recipes.CreateRecipes();

            _world = new WorldBuilder()
                .AddSystem(new TerrainGenerationSystem(GraphicsDevice, Content.Load<TiledMap>("Terrain/BasicTileMap"), _camera))
                .AddSystem(new SpriteRenderSystem(GraphicsDevice, _camera))
                .AddSystem(new PlayerUpdateSystem(_camera))
                .Build();
            Components.Add(_world);

            base.Initialize();
        }

        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            _world.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            // TODO: Add you draw logic here
            _world.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}