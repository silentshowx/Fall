﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall
{
    public static class FigureState
    {
        public const string InGame = "InGame";
        public const string AtBoard = "AtBoard";
        public const string InBase = "InBase";
    }
    public class Figure
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> ActiveFigures = new List<string>();

        public Position CurrentPosition { get; set; }

    }

    public class FigureX
    {
        public string Name { get; set; }
        public string Postion { get; set; }
        public int NumPosition { get; set; }
        public int Count { get; set; }
        public bool InGame { get; set; }

        public bool WaitForInput { get; set; }
    }
}
