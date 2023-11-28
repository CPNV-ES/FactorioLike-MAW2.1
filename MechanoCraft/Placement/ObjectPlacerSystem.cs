using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using MechanoCraft.Input;
using MechanoCraft.Render;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using Microsoft.Xna.Framework.Content;

namespace MechanoCraft.Placement
{
    public class ObjectPlacerSystem
    {
        public static void Place(Entity entity, Vector2 position)
        {
            entity.Get<Transform2>().Position = position;
        }
    }
}
