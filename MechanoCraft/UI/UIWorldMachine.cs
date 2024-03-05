using GeonBit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI;
using GeonBit.UI.Entities;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using MechanoCraft.Input;
using MechanoCraft.Inventory.Items;
using MechanoCraft.Entities.Machines;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using System.Diagnostics;
using Point = Microsoft.Xna.Framework.Point;
using MechanoCraft.Entities.Machines;
using MechanoCraft.Loader;
using System.Reflection.Metadata;
using MechanoCraft.Crafting;
using MechanoCraft.Crafting.Recipes;

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
        private bool hovering;
        private bool createdUI;
        private UIMachineInventory uIMachineInventory;
        private List<Item> results;

        private MouseState currentMouseState;
        private MouseState oldMouseState;
        private Item currentItem;
        private OrthographicCamera camera;
        private Vector2 worldPos;
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
            InputHandler.GetInstance().RegisterLeftMouseButtonListener(() =>
            {
                if (!createdUI && hovering && currentItem != null)
                {
                    uIMachineInventory.BasePanel(currentItem.name);
                    createdUI = true;
                    uIMachineInventory.OneInputOutputUI();
                    Recipe recipe = Recipes.possibleRecipes[0];
                    uIMachineInventory.inputItems = recipe.inputs;
                    uIMachineInventory.ChangeInput(EntityLoadSystem.LoadSprite(uIMachineInventory.inputItems[0].name));
                    uIMachineInventory.button.OnClick = (GeonBit.UI.Entities.Entity entity) => { canCraft = true; results = CraftingSystem.Craft(recipe, recipe.inputs); };
                    uIMachineInventory.panel.OnMouseLeave += OnMouseLeaveUI;
                }

            });
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            hovering = false;
            foreach (var entity in ActiveEntities)
            {
                
                var sprite = _spriteMapper.Get(entity);
                var transform = _transformMapper.Get(entity);
                var machine = _machineMapper.Get(entity);
                var vector = camera.ScreenToWorld(mouseState.X, mouseState.Y);
                var rectangle =  new Rectangle((int)vector.X, (int)vector.Y, sprite.TextureRegion.Texture.Width, sprite.TextureRegion.Texture.Height);

                if (Intersects(rectangle, (Rectangle)sprite.GetBoundingRectangle(transform))&& machine.IsPlaced)
                {
                    currentItem = machine.Item;
                    hovering = true;
                }
            }
            if (UIMachineInventory.progressBar != null && canCraft)
            {
                UIMachineInventory.progressBar.Value += gameTime.ElapsedGameTime.Milliseconds/10;
                if(UIMachineInventory.progressBar.Value >= 100)
                {
                    canCraft = false;

                    uIMachineInventory.ChangeOutput(EntityLoadSystem.LoadSprite(results[0].name));
                }
            }
        }
        public static bool Intersects(Rectangle a, Rectangle b)
        {
            return a.Intersects(b);
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
