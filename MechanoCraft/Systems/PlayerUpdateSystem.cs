using MechanoCraft.Entities.Player;
using MechanoCraft.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace MechanoCraft.Systems
{
    public class PlayerUpdateSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Player> _playerMapper;

        private readonly OrthographicCamera _orthographicCamera;
        private static readonly float PLAYER_BASE_SPEED = 20f;


        public PlayerUpdateSystem(OrthographicCamera orthographicCamera) : base(Aspect.All(typeof(Transform2), typeof(Sprite), typeof(Player)))
        {
            _orthographicCamera = orthographicCamera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _playerMapper = mapperService.GetMapper<Player>();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 mouvementDirection = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
            {
                mouvementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.S))
            {
                mouvementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.A))
            {
                mouvementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown (Keys.D))
            {
                mouvementDirection += Vector2.UnitX;
            }

            foreach (int entity in ActiveEntities)
            {
                _transformMapper.Get(entity).Position += mouvementDirection * PLAYER_BASE_SPEED;
            }
            _orthographicCamera.Move(mouvementDirection * PLAYER_BASE_SPEED);
        }
    }
}
