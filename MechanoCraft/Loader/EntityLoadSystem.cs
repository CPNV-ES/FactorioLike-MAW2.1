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
        public static ContentManager content;

        public static Entity LoadSpriteAsEntity(string spriteName, World _world) 
        {
            Entity entity = _world.CreateEntity();
            entity.Attach(new Transform2());
            entity.Attach(new Sprite(content.Load<Texture2D>(spriteName)));
            entity.Attach(new Machine());
            return entity;
        }
        public static Texture2D LoadSprite(string spriteName)
        {
            return content.Load<Texture2D>(spriteName);
        }
    }
}
