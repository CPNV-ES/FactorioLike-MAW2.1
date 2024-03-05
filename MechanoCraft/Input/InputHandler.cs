using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MechanoCraft.Input
{
    public class InputHandler
    {
        private static InputHandler instance;
        private Dictionary<Keys, Action> keyboardListeners;
        private Dictionary<Buttons, Action> gamepadListeners;
        private Action leftMouseButtonListener;
        private Action rightMouseButtonListener;
        private Action middleMouseButtonListener;
        private Action x1MouseButtonListener;
        private Action x2MouseButtonListener;
        private MouseState oldMouseState;
        private MouseState currentMouseState;
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

        public void RegisterLeftMouseButtonListener(Action listener)
        {
            if (leftMouseButtonListener == null)
            {
                leftMouseButtonListener = listener;
            }
        }

        public void RegisterRightMouseButtonListener(Action listener)
        {
            if(rightMouseButtonListener == null)
            {
                rightMouseButtonListener = listener;
            }
        }

        public void RegisterMiddleMouseButtonListener(Action listener)
        {
            if(middleMouseButtonListener == null)
            {
                middleMouseButtonListener = listener;
            }
        }

        public void RegisterX1MouseButtonListener(Action listener)
        {
            if(x1MouseButtonListener == null)
            {
                x1MouseButtonListener = listener;
            }
        }

        public void RegisterX2MouseButtonListener(Action listener)
        {
            if(x2MouseButtonListener == null)
            {
                x2MouseButtonListener = listener;
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

            MouseState mouseState = Mouse.GetState();

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
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (oldMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed && leftMouseButtonListener != null)
                leftMouseButtonListener();
            if (mouseState.RightButton == ButtonState.Pressed && rightMouseButtonListener != null)
                rightMouseButtonListener();
            if (mouseState.MiddleButton == ButtonState.Pressed && middleMouseButtonListener != null)
                middleMouseButtonListener();
            if (mouseState.XButton1 == ButtonState.Pressed && x1MouseButtonListener != null)
                x1MouseButtonListener();
            if (mouseState.XButton2 == ButtonState.Pressed && x2MouseButtonListener != null)
                x2MouseButtonListener();
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
