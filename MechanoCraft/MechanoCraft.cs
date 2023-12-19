using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MechanoCraft.Input;
using MechanoCraft.Render;
using MonoGame.Extended.Sprites;
using GeonBit.UI;
using MechanoCraft.UI;
using MechanoCraft.Placement;
using MechanoCraft.Loader;
using System.Collections.Generic;
using System;
using System.Reflection.PortableExecutable;
using MechanoCraft.Structs;

namespace MechanoCraft
{
    public class MechanoCraft : Game
    {
        private GraphicsDeviceManager _graphics;
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
            _world = new WorldBuilder().AddSystem(new SpriteRenderSystem(GraphicsDevice)).AddSystem(new UIRenderer(GraphicsDevice,Content)).AddSystem(new UIWorldMachine(GraphicsDevice)).Build();
            Components.Add(_world);
            InputHandler.GetInstance().AddInputListener(Keys.Space, () =>
            {
                Entity entity = EntityLoadSystem.LoadSpriteAsEntity("Crafter", _world, Content);
                entity.Attach(new Structs.Machine());
                ObjectPlacerSystem.Place(entity, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), gridSize);
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