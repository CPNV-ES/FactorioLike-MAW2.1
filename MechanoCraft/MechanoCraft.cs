using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MechanoCraft.Input;
using MechanoCraft.Loader;
using MechanoCraft.Render;
using MonoGame.Extended.Sprites;
using MechanoCraft.Generator;
using MonoGame.Extended.Tiled;
using MechanoCraft.Placement;
using MechanoCraft.Entities.Player;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using MechanoCraft.Entities.Machines;


namespace MechanoCraft
{
    public class MechanoCraft : Game
    {
        private GraphicsDeviceManager _graphics;
        private OrthographicCamera _camera;
        private SpriteBatch _spriteBatch;
        private World _world;
        private Vector2 gridSize = new Vector2(144, 144);
        private Entity currentEntity;
        private Vector2 _worldPos;
        public MechanoCraft()
        {
            _graphics = new GraphicsDeviceManager(this);     
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ObjectPlacerSystem placerSystem = (ObjectPlacerSystem)ObjectPlacerSystem.GetInstance();
            BoxingViewportAdapter viewportAdapter = new BoxingViewportAdapter(Window, _graphics.GraphicsDevice, _graphics.GraphicsDevice.ScissorRectangle.Width, _graphics.GraphicsDevice.ScissorRectangle.Height);
            _camera = new OrthographicCamera(viewportAdapter);

            _world = new WorldBuilder()
                .AddSystem(new SpriteRenderSystem(GraphicsDevice, _camera))
                .AddSystem(new PlayerUpdateSystem(_camera))
                .AddSystem(placerSystem)
                .AddSystem(new TerrainGenerator(GraphicsDevice, Content.Load<TiledMap>("Terrain/BasicTileMap")))
                .Build();
            Components.Add(_world);
            currentEntity = EntityLoadSystem.LoadSpriteAsEntity("Crafter", _world, Content);
            
            InputHandler.GetInstance().AddInputListener(Keys.Space, () =>
            {   if (placerSystem.CanPlaceObject(currentEntity))
                {
                    currentEntity.Get<Machine>().IsPlaced = true;
                    currentEntity = EntityLoadSystem.LoadSpriteAsEntity("Crafter", _world, Content);
                }
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
            var mouseState = Mouse.GetState();
            _worldPos = _camera.ScreenToWorld(new Vector2(mouseState.X, mouseState.Y));
            // TODO: Add your update logic here
            _world.Update(gameTime);
            InputHandler.GetInstance().ProcessListeners();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (currentEntity != null)
            {
                ObjectPlacerSystem.Place(currentEntity, new Vector2(_worldPos.X, _worldPos.Y), gridSize);
            }

            _world.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}