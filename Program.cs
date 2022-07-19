using Grill.DataModel;
using ConsoleTables;

namespace Grill{
    public class Program
    {
    static void Main(string[] args)
    {
        Rectangle G = new Rectangle();
        G.Length = 20;
        G.Width = 30;
        int grillSurfaceArea = G.Length * G.Width;
        List<Menu> sortedMenus = RetrieveMenus.GetMenus().OrderBy(m=>m.menu).ToList();
        foreach(Menu Menu in sortedMenus){
            List<MenuItem> menuItems = Menu.items;
            foreach(MenuItem menuItem in menuItems){
                menuItem.SurfaceArea = menuItem.Length * menuItem.Width;
            }
        }
        //Assuming we can cut up the food and maximise the space used on the grill, filling every cm of the grill
        CalculateRoundsCuttingFood.CutUpFoodAndCalculateRounds(grillSurfaceArea, sortedMenus);
        //Without cutting up the food
        CalculateRoundsProperly.CalculateRoundsWithoutCuttingFood(sortedMenus, G);

        //print pretty table in console
        var table = new ConsoleTable("Menu Number", "Rounds Cut Up", "Rounds Regular");
        foreach(Menu Menu in sortedMenus){
        table.AddRow(Menu.menu, Menu.roundsCutUp, Menu.roundsRegular);
        }
        table.Write();
        Console.WriteLine();
    }
    }
}