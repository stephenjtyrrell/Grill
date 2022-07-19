 using Grill.DataModel;
 
 namespace Grill{
    class CalculateRoundsProperly{
        static List<MenuItem> totalItems = new List<MenuItem>();

        public static void CalculateRoundsWithoutCuttingFood(List<Menu> Menus, Rectangle G)
        {   
            foreach(Menu menu in Menus){
                int rounds = 0;
                List<MenuItem> sortedItems = new List<MenuItem>();
                //order the items by surface area descending, so that larger items are tried first
                sortedItems = menu.items.OrderByDescending(m => m.SurfaceArea).ToList();
                foreach(MenuItem item in sortedItems){
                    for(int i= 0; i < item.Quantity; i++){
                        //add each item individually to a list, removing the quantity field essentially
                        totalItems.Add(new MenuItem{Length = item.Length, Width = item.Width, Quantity = 1, SurfaceArea = item.SurfaceArea});
                    }
                
                }
                int itemsRemaining = totalItems.Count;

                for(int j = 0; j < itemsRemaining; j++){
                //create the full grill object    
                var grill = new Rectangle{Length = G.Length, Width = G.Width};
                //attempt to add items to the grill
                int itemsAdded = AddToGrill(totalItems, grill);
                //once the grill is full, add 1 round to the count
                rounds ++;
                //remove the number of items added to the grill in that round from the remaining items count
                itemsRemaining = (itemsRemaining - itemsAdded);
                }
                //set value on the menu of the number of rounds it took
                menu.roundsRegular = rounds;
            }
        
        }

        static int AddToGrill(List<MenuItem> items, Rectangle grill)
            {
                int count = 0;
                foreach (var item in items.ToList<MenuItem>())
                { 
                    //try find an open space in the grill
                    var spaceOnGrill = FindSpaceOnGrill(grill, item.Width, item.Length);
                    //if there's an open space
                    if (spaceOnGrill != null)
                    {   
                        //try fit it on a rectangle portion of the grill
                        item.position = SplitGrillSpace(spaceOnGrill, item.Width, item.Length);
                        //add to the count of items added in this round
                        count++;
                        //remove from the list of remaining items to add to the grill for this menu
                        totalItems.Remove(item);
                    }
                }
                //return the number of items added in this round total
                return count;
            }

        static Rectangle FindSpaceOnGrill(Rectangle grill, int itemWidth, int itemLength)
        {
            //if this part of the grill is occupied
            if (grill.isOccupied) 
            {
                //try the bottom corner
                var nextSpace = FindSpaceOnGrill(grill.bottomCorner, itemWidth, itemLength);

                //if that's null try the right corner
                if (nextSpace == null)
                {
                    nextSpace = FindSpaceOnGrill(grill.rightCorner, itemWidth, itemLength);
                }

                return nextSpace;
            }
            //if that space is not occupied and the item can fit in it by horizontal or vertical orientation
            else if ((itemWidth <= grill.Width && itemLength <= grill.Length) || (itemWidth <= grill.Length && itemLength <= grill.Width))
            {
                return grill;
            }
            else 
            {
                return null;
            }
        }

        static Rectangle SplitGrillSpace(Rectangle grill, int itemWidth, int itemLength)
        {
            grill.isOccupied = true;
            grill.bottomCorner = new Rectangle { pos_z = grill.pos_z, pos_x = grill.pos_x + itemLength,Length = grill.Length, Width = grill.Width - itemWidth };
            grill.rightCorner = new Rectangle {  pos_z = grill.pos_z + itemLength, pos_x = grill.pos_x,Length = grill.Length - itemLength, Width = itemWidth };
            return grill;
        }

    }
 }
 