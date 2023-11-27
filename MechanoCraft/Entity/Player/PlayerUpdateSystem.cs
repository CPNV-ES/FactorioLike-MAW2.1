using MechanoCraft.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace MechanoCraft.Entity.Player
{
    public class PlayerUpdateSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Player> _playerMapper;

        private static readonly Vector2 UNIT_X_VECTOR = new Vector2(1f, 0f);
        private static readonly Vector2 UNIT_Y_VECTOR  = new Vector2(0f, 1f);
        private static readonly float PLAYER_BASE_SPEED = 20f;
        public PlayerUpdateSystem() : base(Aspect.All(typeof(Transform2), typeof(Sprite), typeof(Player)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _playerMapper = mapperService.GetMapper<Player>();

            InputHandler.GetInstance().AddInputListener(Microsoft.Xna.Framework.Input.Keys.W, () =>
            {
                foreach (int entity in ActiveEntities)
                {
                    _transformMapper.Get(entity).Position += UNIT_Y_VECTOR * -PLAYER_BASE_SPEED;
        }
            });

            InputHandler.GetInstance().AddInputListener(Microsoft.Xna.Framework.Input.Keys.S, () =>
            {
                foreach (int entity in ActiveEntities)
                {
                    _transformMapper.Get(entity).Position += UNIT_Y_VECTOR * PLAYER_BASE_SPEED;
                }
            });
            InputHandler.GetInstance().AddInputListener(Microsoft.Xna.Framework.Input.Keys.A, () =>
            {
                foreach (int entity in ActiveEntities)
                {
                    _transformMapper.Get(entity).Position += UNIT_X_VECTOR * -PLAYER_BASE_SPEED;
                }
            });
            InputHandler.GetInstance().AddInputListener(Microsoft.Xna.Framework.Input.Keys.D, () =>
            {
                foreach (int entity in ActiveEntities)
                {
                    _transformMapper.Get(entity).Position += UNIT_X_VECTOR * PLAYER_BASE_SPEED;
                }
            });
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
