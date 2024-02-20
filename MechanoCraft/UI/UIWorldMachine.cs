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

namespace MechanoCraft.UI
{
    public class UIWorldMachine : EntityUpdateSystem
    {
        bool a = false;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Machine> _machineMapper;
        private bool hovering;
        private bool createdUI;
        private UIMachineInventory uIMachineInventory;

        private MouseState currentMouseState;
        private MouseState oldMouseState;
        private Item currentItem;
        public UIWorldMachine(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(Transform2), typeof(Sprite), typeof(Machine)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _machineMapper = mapperService.GetMapper<Machine>();
            uIMachineInventory = new UIMachineInventory();
            InputHandler.GetInstance().RegisterLeftMouseButtonListener(() =>
            {

                if (hovering && !createdUI && currentMouseState.LeftButton == ButtonState.Pressed && !(oldMouseState.LeftButton == ButtonState.Pressed))
                {
                    uIMachineInventory.BasePanel(currentItem.name);
                    uIMachineInventory.OneInputOutputUI();
                    createdUI = true;
                }
            });

        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Microsoft.Xna.Framework.Point(mouseState.X, mouseState.Y);
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            foreach (var entity in ActiveEntities)
            {
                currentItem = _machineMapper.Get(entity).Item;
                var sprite = _spriteMapper.Get(entity);

                if (sprite.TextureRegion.Bounds.Contains(mousePoint))
                {
                    hovering = true;
                }
                else
                {
                    hovering = false;
                    createdUI = false;
                    uIMachineInventory.DeletePanel();
                }
            }
        }
    }
}
