using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MechanoCraft.Input;
using MechanoCraft.Loader;
using MechanoCraft.Render;
using MonoGame.Extended.Sprites;
using MechanoCraft.Crafting;
using MechanoCraft.Crafting.Recipes;
using MechanoCraft.Inventory;
using MechanoCraft.Inventory.Items;
using MechanoCraft.Generator;
using MonoGame.Extended.Tiled;
using MechanoCraft.Placement;
using MechanoCraft.Entities.Player;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using MechanoCraft.Entities.Machines;
using System.Collections.Generic;
using MechanoCraft.UI;
using GeonBit.UI;
using Microsoft.Xna.Framework.Content;

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

            ItemCreator.CreateItems();
            Recipes.CreateRecipes();

            List<Item> items = new List<Item>
            {
                ItemCreator.possibleItems[1],
            };
            UserInterface.Initialize(Content, BuiltinThemes.hd);

            List<Item> output = CraftingSystem.Craft(Recipes.possibleRecipes[0], items);
            _world = new WorldBuilder()
                .AddSystem(new TerrainGenerator(GraphicsDevice, Content.Load<TiledMap>("Terrain/BasicTileMap"), _camera))
                .AddSystem(new SpriteRenderSystem(GraphicsDevice, _camera))
                .AddSystem(new PlayerUpdateSystem(_camera))
                .AddSystem(new UIRenderer(GraphicsDevice, Content))
                .AddSystem(new UIWorldMachine(GraphicsDevice, _camera))
                .AddSystem(placerSystem)
                .Build();
            Components.Add(_world);
            EntityLoadSystem.content = Content;


            currentEntity = EntityLoadSystem.LoadSpriteAsEntity("Crafter", _world);

            InputHandler.GetInstance().AddInputListener(Keys.Q, () =>
            {
                currentEntity.Get<Machine>().Name = "Crafter";
                currentEntity.Detach<Sprite>();
                currentEntity.Attach(new Sprite(EntityLoadSystem.LoadSprite("Crafter")));
            });

            InputHandler.GetInstance().AddInputListener(Keys.E, () =>
            {
                currentEntity.Get<Machine>().Name = "smelter";
                currentEntity.Detach<Sprite>();
                currentEntity.Attach(new Sprite(EntityLoadSystem.LoadSprite("smelter")));
            });

            InputHandler.GetInstance().AddInputListener(Keys.Space, () =>
            {   if (placerSystem.CanPlaceObject(currentEntity))
                {
                    Machine machine = currentEntity.Get<Machine>();
                    machine.IsPlaced = true;
                    switch (machine.Name)
                    {
                        case "Crafter":
                            machine.Recipe = Recipes.possibleRecipes[1];
                            break;
                        case "smelter":
                            machine.Recipe = Recipes.possibleRecipes[0];
                            break;
                    }
                    currentEntity = EntityLoadSystem.LoadSpriteAsEntity(machine.Name, _world);
                }
            });
            InputHandler.GetInstance().AddInputListener(Keys.Escape, () =>
            {
                Exit();
            });
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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
            GraphicsDevice.Clear(Color.DarkGray);
            _world.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}