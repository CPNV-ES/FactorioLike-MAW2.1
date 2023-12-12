using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI.Entities;
using Entity = GeonBit.UI.Entities.Entity;
using GeonBit.UI;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace MechanoCraft.Render
{
    public class UIRenderer : EntityDrawSystem, IUpdate
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ContentManager _contentManager;
        public UIRenderer(GraphicsDevice graphicsDevice, ContentManager contentManager)
            : base(Aspect.All(typeof(Transform2), typeof(Entity)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _contentManager = contentManager;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            UserInterface.Initialize(_contentManager, BuiltinThemes.hd);
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Draw(GameTime gameTime)
        {

            UserInterface.Active.Draw(_spriteBatch);

        }

        public void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
        }
    }
}
