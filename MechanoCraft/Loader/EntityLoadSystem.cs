using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Content;
using MechanoCraft.Entities.Machines;

namespace MechanoCraft.Loader
{
    public static class EntityLoadSystem
    {

        public static Entity LoadSpriteAsEntity(string spriteName, World _world, ContentManager Content) 
        {
            Entity entity = _world.CreateEntity();
            entity.Attach(new Transform2());
            entity.Attach(new Sprite(Content.Load<Texture2D>(spriteName)));
            entity.Attach(new Machine());
            return entity;
        }
    }
}
