using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using MechanoCraft.Input;

namespace MechanoCraft
{
    public class MechanoCraft : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private World _world;
        private Dictionary<SpriteRenderer, Vector2> spriteRenderers = new Dictionary<SpriteRenderer, Vector2>(); 

        public MechanoCraft()
        {
            _graphics = new GraphicsDeviceManager(this);     
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here
            _world = new WorldBuilder().AddSystem(new RenderSystem(GraphicsDevice)).Build();
            Components.Add(_world);
            spriteRenderers.Add(new SpriteRenderer(Content.Load<Texture2D>("minerai"), Color.White), new Vector2(10, 10));
            spriteRenderers.Add(new SpriteRenderer(Content.Load<Texture2D>("MinerShadow"), Color.White), new Vector2(10, 10));
            spriteRenderers.Add(new SpriteRenderer(Content.Load<Texture2D>("Miner"), Color.White), new Vector2(10, 10));
            spriteRenderers.Add(new SpriteRenderer(Content.Load<Texture2D>("crafter"), Color.White), new Vector2(80, 10));
            spriteRenderers.Add(new SpriteRenderer(Content.Load<Texture2D>("polish"), Color.White), new Vector2(60, 10));
            spriteRenderers.Add(new SpriteRenderer(Content.Load<Texture2D>("smelter"), Color.White), new Vector2(40, 10));
            foreach (var spriteRenderer in spriteRenderers)
            {
                var entity = _world.CreateEntity();
                entity.Attach(new Transform2(spriteRenderer.Value));
                entity.Attach(spriteRenderer.Key);
            }

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