using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace MechanoCraft.Placement
{
    public static class ObjectPlacerSystem
    {
        public static void Place(Entity entity, Vector2 position, Vector2 gridSize) 
        {
            int TileX = (int)(position.X / gridSize.X);
            int TileY = (int)(position.Y / gridSize.Y);
            entity.Get<Transform2>().Position = new Vector2(TileX* gridSize.X,TileY * gridSize.Y);
        }
    }
}
