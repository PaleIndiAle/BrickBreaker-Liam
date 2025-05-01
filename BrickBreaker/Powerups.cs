using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    internal class Powerups
    {
        Paddle paddle;

        public Powerups()
        {
            
        }
        // Makes the player move two pixels faster
        public void Speed_I()
        {
            paddle.speed = 8 + 2;
        }
        // Makes the player move four pixels faster
        public void Speed_II()
        {
            paddle.speed = 8 + 4;
        }
        // Makes the player move six pixels faster
        public void Speed_III()
        {
            paddle.speed = 8 + 6;
        }
        // Adds a life to the player
        public void Golden_Carrot()
        {
            if (GameScreen.lives > 3)
            {
                GameScreen.lives++;
            }
        }
        // Adds three life to the player
        public void Golden_Apple()
        {
            if (GameScreen.lives >= 3)
            {
                GameScreen.lives = 4;
            }
        }
        // Adds one more ball to the field
        public void Slime()
        {
            GameScreen.SlimeNum = 1;
        }
        // Upgrades to stone tool, does 1 extra damage
        // Upgrades to iron tool, does 2 extra damage
        // Upgrades to diamond tool, does 3 extra damage
        // Upgrades to netherite tool, does 4 extra damage, can break unbreakable blocks
    }
}
