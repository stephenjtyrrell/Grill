using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grill.DataModel
{

    public class Menu
    {
        public string Id { get; set; }
        public string menu { get; set; }
        public List<MenuItem> items { get; set; }
        public int roundsCutUp {get; set;}
        public int roundsRegular {get; set;}
    }

    public class MenuItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public string Duration { get; set; }
        public int Quantity { get; set; }
        public int SurfaceArea {get; set; }

        public Rectangle position;

    }
    public class Rectangle
    {
        public int pos_x;
        public int pos_z;
        public Rectangle rightCorner { get; set;}
        public Rectangle bottomCorner { get; set;}
        public int Length {get; set; }
        public int Width {get; set;}
        public bool isOccupied {get; set;}
    }
}
