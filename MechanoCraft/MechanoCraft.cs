using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using MechanoCraft.Input;
using MechanoCraft.Render;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

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
            _world = new WorldBuilder().AddSystem(new SpriteRenderSystem(GraphicsDevice)).Build();
            Components.Add(_world);
            Entity entity = _world.CreateEntity();
            InputHandler.GetInstance().AddInputListener(Keys.Space, () =>
            {
                Entity entity1 = _world.CreateEntity();
                entity1.Attach( new Transform2());
                entity1.Attach( new Sprite(Content.Load<Texture2D>("Crafter")));
                entity1.Get<Transform2>().Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
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