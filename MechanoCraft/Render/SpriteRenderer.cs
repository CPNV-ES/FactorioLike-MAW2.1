using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanoCraft
{
    public class RenderSystem : EntityDrawSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<SpriteRenderer> _spriteRendererMapper;

        public RenderSystem(GraphicsDevice graphicsDevice)
            : base(Aspect.All(typeof(Transform2), typeof(SpriteRenderer)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteRendererMapper = mapperService.GetMapper<SpriteRenderer>();
        }

        public override void Draw(GameTime gameTime)
        {
            _graphicsDevice.Clear(Color.Wheat);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var spriteRenderer = _spriteRendererMapper.Get(entity);

                Vector2 origin;
                if (spriteRenderer.drawMode == SpriteRenderer.DrawMode.Center)
                {
                    origin = new Vector2(spriteRenderer.Sprite.Width/2,spriteRenderer.Sprite.Height / 2);
                }
                else
                {
                    origin = Vector2.Zero;
                }

                _spriteBatch.Draw(spriteRenderer.Sprite, transform.Position, null, spriteRenderer.Color, transform.Rotation, origin, transform.Scale, SpriteEffects.None, spriteRenderer.Layer);
            }
            _spriteBatch.End();
        }
    }
}
