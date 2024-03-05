using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MechanoCraft.Entities.Machines;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MechanoCraft.Systems
{
    public class ObjectPlacerSystem : EntityUpdateSystem
    {
        private OrthographicCamera _camera;
        private readonly ContentManager _contentManager;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Machine> _machineMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private Entity _previewEntity;
        private Vector2 _gridSize;

        public ObjectPlacerSystem(OrthographicCamera camera, Vector2 gridSize, ContentManager contentManager) : base(Aspect.All(typeof(Transform2), typeof(Sprite), typeof(Machine)))
        {
            _camera = camera;
            _gridSize = gridSize;
            _contentManager = contentManager;
        }

        public bool CanPlaceObject(Entity entity)
        {
            foreach (var existingObject in ActiveEntities)
            {
                if (existingObject != entity.Id)
                {
                    var sprite = _spriteMapper.Get(existingObject);
                    var transform = _transformMapper.Get(existingObject);                

                    if (entity.Get<Sprite>().GetBoundingRectangle(entity.Get<Transform2>()).Intersects(sprite.GetBoundingRectangle(transform)))
                    {
                        entity.Get<Sprite>().Color = new Color(155, 36, 31);
                        // Collision detected, cannot place object here
                        return false;
                    }
                }
            }
            entity.Get<Sprite>().Color = new Color(255, 255, 255);
            // No collision detected, can place object
            return true;
        }


        public bool Intersects(Rectangle a, Rectangle b)
        {
            Rectangle rec = new Rectangle(b.X, b.Y, 1, 1);
            Rectangle rec2 = new Rectangle(a.X, a.Y, 1, 1);
            return rec2.Intersects(rec);
        }

        public void Place(Entity entity, Vector2 position)
        {
            CanPlaceObject(entity);
            entity.Get<Transform2>().Position = new Vector2((position.X - (position.X % _gridSize.X)) + _gridSize.X / 2, (position.Y - (position.Y % _gridSize.Y)) + _gridSize.Y / 2);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _machineMapper = mapperService.GetMapper<Machine>();
            _transformMapper = mapperService.GetMapper<Transform2>();
            _previewEntity = CreateEntity();
            _previewEntity.Attach(new Transform2());
            _previewEntity.Attach(new Sprite(_contentManager.Load<Texture2D>("Crafter")));
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            Place(_previewEntity, ScreenToWorldSpace(mouseState.Position));
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (CanPlaceObject(_previewEntity))
                {
                    _previewEntity.Attach(new Machine());
                    _previewEntity.Get<Machine>().IsPlaced = true;

                    _previewEntity = CreateEntity();
                    _previewEntity.Attach(new Transform2(ScreenToWorldSpace(mouseState.Position)));
                    _previewEntity.Attach(new Sprite(_contentManager.Load<Texture2D>("Crafter")));
                } else
                {
                    //DestroyEntity(entity.Id);
                }
            }
        }

        private Vector2 ScreenToWorldSpace(Point point)
        {
            return Vector2.Transform(point.ToVector2(), _camera.GetInverseViewMatrix());
        }
    }
}
