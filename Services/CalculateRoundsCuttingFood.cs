using Grill.DataModel;

namespace Grill{
    class CalculateRoundsCuttingFood{
        public static void CutUpFoodAndCalculateRounds(int grillSurfaceArea, List<Menu>Menus)
        {
            foreach(Menu fullMenu in Menus){
                List<MenuItem> menuItems = fullMenu.items;
                int totalSurfaceAreaFood = new int();
                foreach(MenuItem menuItem in menuItems){
                    //calculate the surface area of each item
                    totalSurfaceAreaFood = totalSurfaceAreaFood + (menuItem.SurfaceArea * menuItem.Quantity);
                }
                //calculate the rounds needed by dividing the total surface area of the food by the surface area of the grill
                decimal roundDecimals = Decimal.Divide(totalSurfaceAreaFood,grillSurfaceArea);
                //round up to nearest integer
                int rounds = (int)Math.Ceiling(roundDecimals);

                //set value on menu object to be used to print table
                fullMenu.roundsCutUp = rounds;

            }
        }
    }
}