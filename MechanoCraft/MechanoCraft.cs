using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MechanoCraft.Input;
using MechanoCraft.Loader;
using MechanoCraft.Render;
using MechanoCraft.Placement;
using MechanoCraft.Entities.Player;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;

namespace MechanoCraft
{
    public class MechanoCraft : Game
    {
        private GraphicsDeviceManager _graphics;
        private OrthographicCamera _camera;
        private SpriteBatch _spriteBatch;
        private World _world;
        private Vector2 gridSize = new Vector2(144, 144);
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

            _world = new WorldBuilder()
                .AddSystem(new SpriteRenderSystem(GraphicsDevice, _camera))
                .AddSystem(new PlayerUpdateSystem(_camera))
                .Build();
            Components.Add(_world);
            InputHandler.GetInstance().AddInputListener(Keys.Space, () =>
            {
                var mouseState = Mouse.GetState();
                var _worldPosition = _camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
                ObjectPlacerSystem.Place(EntityLoadSystem.LoadSpriteAsEntity("Crafter", _world, Content), new Vector2(_worldPosition.X, _worldPosition.Y), gridSize);
            });
            InputHandler.GetInstance().AddInputListener(Keys.Escape, () =>
            {
                Exit();
            });

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
            InputHandler.GetInstance().ProcessListeners();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _world.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}