using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fall
{
    public static class WhichPlayer
    {
        public const string None = "None";
        public const string Yellow = "Yellow";
        public const string Green = "Green";
        public const string Red = "Red";
        public const string Black = "Black";
    }
    internal class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int LastNumber { get; set; }

        public bool IsBingo { get; set; }
       
        public List<Figure> ActiveFigures = new List<Figure>();
    }
}
