using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MechanoCraft
{
    public class SpriteRenderer
    {
        Texture2D sprite;
        Color color;
        bool flip;

        public SpriteRenderer(Texture2D sprite, Color color, bool flip = false )
        {
            Sprite = sprite;
            Color = color;
            Flip = flip;

        }

        public Texture2D Sprite 
        {
            get { return sprite; } 
            set { sprite = value; } 
        }
        public Color Color 
        { 
            get { return color; }
            set {  color = value; }
        }
        public bool Flip 
        { 
            get { return flip; } 
            set {  flip = value; } 
        }
    }
}
