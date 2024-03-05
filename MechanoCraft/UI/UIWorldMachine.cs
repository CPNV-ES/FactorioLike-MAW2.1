using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using MechanoCraft.Inventory.Items;
using MechanoCraft.Entities.Machines;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using MechanoCraft.Crafting;
using MechanoCraft.Crafting.Recipes;
using MechanoCraft.Loader;

namespace MechanoCraft.UI
{
    public class UIWorldMachine : EntityUpdateSystem
    {
        bool canCraft = false;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Machine> _machineMapper;
        private bool createdUI;
        private UIMachineInventory uIMachineInventory;
        private List<Item> results;

        private Item currentItem;
        private OrthographicCamera camera;

        public UIWorldMachine(GraphicsDevice graphicsDevice, OrthographicCamera camera)
            : base(Aspect.All(typeof(Transform2), typeof(Sprite), typeof(Machine)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            this.camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _machineMapper = mapperService.GetMapper<Machine>();
            uIMachineInventory = new UIMachineInventory();
            results = new List<Item>();    
        }

        public override void Update(GameTime gameTime)
        {
            // Get mouse state
            MouseState mouseState = Mouse.GetState();
            // Loop through all entities to see if the mouse clicked on something
            foreach (int entity in ActiveEntities)
            {
                Sprite sprite = _spriteMapper.Get(entity);
                Transform2 transform = _transformMapper.Get(entity);
                Machine machine = _machineMapper.Get(entity);
                Rectangle mouseCollisionBox = new Rectangle((int)camera.ScreenToWorld(mouseState.Position.ToVector2()).X, (int)camera.ScreenToWorld(mouseState.Position.ToVector2()).Y, 1, 1);

                // Does the 1 by 1 rectangle representing the mouse hit something
                if (mouseCollisionBox.Intersects(sprite.GetBoundingRectangle(transform).ToRectangle()))
                {
                    // Did the user click on this object
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        // Does the UI exist
                        if (!createdUI)
                        {
                            uIMachineInventory.BasePanel(machine.ToString());
                            createdUI = true;
                            uIMachineInventory.OneInputOutputUI();
                            Recipe recipe = Recipes.possibleRecipes[0];
                            uIMachineInventory.inputItems = recipe.inputs;
                            uIMachineInventory.ChangeInput(sprite.TextureRegion.Texture);
                            uIMachineInventory.button.OnClick = (GeonBit.UI.Entities.Entity entity) => { canCraft = true; results = CraftingSystem.Craft(recipe, recipe.inputs); };
                            uIMachineInventory.panel.OnMouseLeave += OnMouseLeaveUI;
                        }
                    }
                }
            }

            if (UIMachineInventory.progressBar != null && canCraft)
            {
                UIMachineInventory.progressBar.Value += gameTime.ElapsedGameTime.Milliseconds / 10;
                if (UIMachineInventory.progressBar.Value >= 100)
                {
                    canCraft = false;

                    uIMachineInventory.ChangeOutput(EntityLoadSystem.LoadSprite(results[0].name));
                }
            }
        }
        private void OnMouseHoverUI(GeonBit.UI.Entities.Entity entity)
        {
            uIMachineInventory.DeletePanel();
        }
        private void OnMouseLeaveUI(GeonBit.UI.Entities.Entity entity)
        {
            createdUI = false;
            uIMachineInventory.DeletePanel();
        }
    }
}
