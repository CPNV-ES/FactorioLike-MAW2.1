using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MechanoCraft.Input
{
    public class InputHandler
    {
        private static InputHandler instance;
        private Dictionary<Keys, Action> keyboardListeners;
        private Dictionary<Buttons, Action> gamepadListeners;
        private InputHandler()
        {
            keyboardListeners = new Dictionary<Keys, Action>();
            gamepadListeners = new Dictionary<Buttons, Action>();
        }

        public void AddInputListener(Keys key, Action listener)
        {
            if(keyboardListeners != null && !keyboardListeners.ContainsKey(key))
            {
                keyboardListeners.Add(key, listener);
            }
        }

        public void AddInputListener(Buttons button, Action listener)
        {
            if(gamepadListeners != null && !gamepadListeners.ContainsKey(button))
            {
                gamepadListeners.Add(button, listener);
            }
        }

        public void RemoveInputListener(Keys key)
        {
            if(keyboardListeners != null && keyboardListeners.ContainsKey(key))
            {
                keyboardListeners.Remove(key);
            }
        }

        public void RemoveInputListener(Buttons button)
        {
            if(gamepadListeners != null && gamepadListeners.ContainsKey(button))
            {
                gamepadListeners.Remove(button);
            }
        }

        public void ProcessListeners()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // We only listen for user 1 at the moment (implement later)
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            foreach (KeyValuePair<Keys, Action> listener in keyboardListeners)
            {
                if(keyboardState.IsKeyDown(listener.Key))
                {
                    listener.Value();
                }
            }

            foreach (KeyValuePair<Buttons, Action> listener in gamepadListeners)
            {
                if(gamePadState.IsConnected && gamePadState.IsButtonDown(listener.Key))
                {
                    listener.Value();
                }
            }
        }

        public static InputHandler GetInstance()
        {
            if(instance == null)
            {
                instance = new InputHandler();
            }
            return instance;
        }
    }
}
