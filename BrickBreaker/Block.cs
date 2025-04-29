using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.Dynamic;

namespace BrickBreaker
{
    public class Block
    {
        public int width = 50;
        public int height = 25;

        public int x;
        public int y; 
        public int hp;
        public Color colour;
        public int level = 1;

        public static Random rand = new Random();

        public Block(int _x, int _y, int _hp, Color _colour)
        {
            x = _x;
            y = _y;
            hp = _hp;
            colour = _colour;
        }

        //public void SetLevel()
        //{
        //    XmlReader reader = XmlReader.Create($"level{level}.xml");

        //    while (reader.Read())
        //    {
        //        Block b = new Block();

        //        reader.ReadToFollowing("brick");
        //        b.x = Convert.ToInt16(reader.GetAttribute("x"));
        //        b.y = Convert.ToInt16(reader.GetAttribute("y"));
        //        b.hp = Convert.ToInt16(reader.GetAttribute("hp"));
        //       // b.colour = Convert.ToColourreader.GetAttribute("colour")
        //    }
        //}
    }


}
