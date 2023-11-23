using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace MechanoCraft.Entity.Player
{
    public class PlayerDrawSystem : EntityDrawSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Player> _playerMapper;

        public PlayerDrawSystem(GraphicsDevice graphicsDevice) : base(Aspect.All(typeof(Transform2), typeof(Player)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach (var entity in ActiveEntities)
            {
                Transform2 transform = _transformMapper.Get(entity);
                Player player = _playerMapper.Get(entity);

                _spriteBatch.Draw(player.PlayerSprite, transform);
            }
            _spriteBatch.End();
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _playerMapper = mapperService.GetMapper<Player>();
        }
    }
}
