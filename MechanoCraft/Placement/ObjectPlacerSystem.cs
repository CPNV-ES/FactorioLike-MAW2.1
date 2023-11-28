using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended;


namespace MechanoCraft.Placement
{
    public static class ObjectPlacerSystem
    {
        public static void Place(Entity entity, Vector2 position)
        {
            entity.Get<Transform2>().Position = position;
        }
    }
}
