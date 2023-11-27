using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanoCraft.Entity.Player
{
    public class Player
    {
        public int Health = 100;
        public Sprite PlayerSprite;

        public Player(int health)
        {
            Health = health;
            PlayerSprite = playerSprite;
        }
    }
}
