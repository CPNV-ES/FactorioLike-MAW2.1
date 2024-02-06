using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MechanoCraft.Entities.Machines;

namespace MechanoCraft.Systems
{
    public class ObjectPlacerSystem : EntitySystem
    {
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Machine> _machineMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private static ObjectPlacerSystem instance;

        public ObjectPlacerSystem(AspectBuilder aspectBuilder) : base(aspectBuilder)
        {
        }

        public bool CanPlaceObject(Entity entity)
        {
            foreach (var existingObject in ActiveEntities)
            {
                var sprite = _spriteMapper.Get(existingObject);
                var machine = _machineMapper.Get(existingObject);
                var transform = _transformMapper.Get(existingObject);
                if (machine.IsPlaced)
                {
                    if (Intersects((Rectangle)entity.Get<Sprite>().GetBoundingRectangle(entity.Get<Transform2>()), (Rectangle)sprite.GetBoundingRectangle(transform)))
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


        public static bool Intersects(Rectangle a, Rectangle b)
        {
            Rectangle rec = new Rectangle(b.X, b.Y, 1, 1);
            Rectangle rec2 = new Rectangle(a.X, a.Y, 1, 1);
            return rec2.Intersects(rec);
        }

        public static void Place(Entity entity, Vector2 position, Vector2 gridSize)
        {
            instance.CanPlaceObject(entity);
            int TileX = (int)(position.X / gridSize.X);
            int TileY = (int)(position.Y / gridSize.Y);
            entity.Get<Transform2>().Position = new Vector2(TileX * gridSize.X, TileY * gridSize.Y);
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _machineMapper = mapperService.GetMapper<Machine>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }
        public static ISystem GetInstance()
        {
            if (instance == null)
            {
                instance = new ObjectPlacerSystem(new AspectBuilder());
            }
            return instance;
        }
    }
}
