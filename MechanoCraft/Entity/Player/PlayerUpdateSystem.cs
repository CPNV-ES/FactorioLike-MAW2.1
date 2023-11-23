using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;

namespace MechanoCraft.Entity.Player
{
    public class PlayerUpdateSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Player> _playerMapper;
        public PlayerUpdateSystem() : base(Aspect.All(typeof(Transform2), typeof(Player)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _playerMapper = mapperService.GetMapper<Player>();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
