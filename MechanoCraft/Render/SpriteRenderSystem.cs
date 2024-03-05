using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace MechanoCraft.Render
{
    public class SpriteRenderSystem : EntityDrawSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly OrthographicCamera _orthographicCamera;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;

        public SpriteRenderSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera)
            : base(Aspect.All(typeof(Transform2), typeof(Sprite)))
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _graphicsDevice = graphicsDevice;
            _orthographicCamera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _orthographicCamera.GetViewMatrix());
            
            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var sprite = _spriteMapper.Get(entity);

                _spriteBatch.Draw(sprite, transform.WorldPosition);
            }
            _spriteBatch.End();

        }
    }
}
