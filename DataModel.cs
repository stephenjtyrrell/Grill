using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrillSolution
{
    public class Menu
    {
        public string Id { get; set; }
        public string menu { get; set; }
        public List<MenuItem> items { get; set; }
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

        public Grill position;

    }
    public class Grill
    {
                    public int pos_x;
            public int pos_z;
        public Grill rightCorner { get; set;}
        public Grill bottomCorner { get; set;}
        public int Length {get; set; }
        public int Width {get; set;}
        public bool isOccupied {get; set;}
    }
}
