using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    internal class Powerups
    {
        public static int poweractivate;

        public Powerups()
        {
            
        }
        // Makes the player move two pixels faster
        public static void Speed_I(Paddle p)
        {
            p.speed = 8 + 2;
        }
        // Makes the player move four pixels faster
        public static void Speed_II(Paddle p)
        {
            p.speed = 8 + 4;
        }
        // Makes the player move six pixels faster
        public static void Speed_III(Paddle p)
        {
            p.speed = 8 + 6;
        }
        // Adds a life to the player
        public static void Golden_Carrot()
        {
            if (GameScreen.lives > 3)
            {
                GameScreen.lives++;
            }
        }
        // Adds three life to the player
        public static void Golden_Apple()
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
