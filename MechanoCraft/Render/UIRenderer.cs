using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Entities;
using MonoGame.Extended;
using GeonBit.UI;
using Microsoft.Xna.Framework.Content;

namespace MechanoCraft.Render
{
    public class UIRenderer : IUpdateSystem, IDrawSystem
    {

        private readonly SpriteBatch _spriteBatch;
        private ContentManager _contentManager;
        public UIRenderer(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _contentManager = contentManager;
        }

        public void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
        }

        public void Dispose()
        {
        }

        public void Draw(GameTime gameTime)
        {
            UserInterface.Active.Draw(_spriteBatch);
        }

        public void Initialize(World world)
        {
            UserInterface.Initialize(_contentManager, BuiltinThemes.hd);
        }
    }
}
