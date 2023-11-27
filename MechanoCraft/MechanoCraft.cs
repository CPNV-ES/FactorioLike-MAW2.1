using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using MechanoCraft.Input;
using MechanoCraft.Render;
using MonoGame.Extended.Sprites;
using MechanoCraft.Entity.Player;

namespace MechanoCraft
{
    public class MechanoCraft : Game
    {
        private GraphicsDeviceManager _graphics;
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
            _world = new WorldBuilder()
                .AddSystem(new SpriteRenderSystem(GraphicsDevice))
                .AddSystem(new PlayerUpdateSystem())
                .Build();
            Components.Add(_world);

            MonoGame.Extended.Entities.Entity playerEntity = _world.CreateEntity();
            playerEntity.Attach(new Transform2(200f, 200f));
            playerEntity.Attach(new Sprite(Content.Load<Texture2D>("minerai")));
            playerEntity.Attach(new Player(100));

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