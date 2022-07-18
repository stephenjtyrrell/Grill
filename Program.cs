using GrillSolution;
using Newtonsoft.Json;
using RestSharp;
using RectpackSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

    class Program
{
    static List<MenuItem> totalItems = new List<MenuItem>();
    static void Main(string[] args)
    {
        Grill G = new Grill();
        G.Length = 20;
        G.Width = 30;
        int grillSurfaceArea = G.Length * G.Width;
        List<Menu> sortedMenus = GetMenus().OrderBy(m=>m.menu).ToList();
        foreach(Menu Menu in sortedMenus){
            List<MenuItem> menuItems = Menu.items;
            foreach(MenuItem menuItem in menuItems){
                menuItem.SurfaceArea = menuItem.Length * menuItem.Width;
            }
        }

        //Assuming we can cut up the food and maximise the space used on the grill, filling every cm of the grill
        CutUpFoodAndCalculateRounds(grillSurfaceArea, sortedMenus);
        //Without cutting up the food
        CalculateRoundsWithoutCuttingFood(sortedMenus, G);
    }

    static List<Menu> GetMenus()
    {
        List<Menu> Menus = new List<Menu>();
        var client = new RestClient("http://isol-grillassessment.azurewebsites.net/api/");
        var request = new RestRequest("GrillMenu");
        var response = client.Execute(request);
        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var MenuResponse = response.Content;
            Menus = JsonConvert.DeserializeObject<List<Menu>>(MenuResponse);
        }
        else{
            throw new Exception("No menus founds");
        }
        return Menus;
    }

    static void CutUpFoodAndCalculateRounds(int grillSurfaceArea, List<Menu>Menus)
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
            var rounds = Math.Ceiling(roundDecimals);
            Console.WriteLine(fullMenu.menu + " will take " + rounds + " rounds, if we cut up the food");

        }
    }

    static void CalculateRoundsWithoutCuttingFood(List<Menu> Menus, Grill G)
    {   
        foreach(Menu menu in Menus){
            int rounds = 0;
            List<MenuItem> sortedItems = new List<MenuItem>();
            sortedItems = menu.items.OrderByDescending(m => m.SurfaceArea).ToList();
            foreach(MenuItem item in sortedItems){
                for(int i= 0; i < item.Quantity; i++){
                    totalItems.Add(new MenuItem{Length = item.Length, Width = item.Width, Quantity = 1, SurfaceArea = item.SurfaceArea});
                }
            
            }
            int itemsRemaining = totalItems.Count;

            for(int j = 0; j < itemsRemaining; j++){
            var grill = new Grill{Length = G.Length, Width = G.Width};
            int itemsAdded = AddToGrill(totalItems, grill);
            rounds ++;
            itemsRemaining = (itemsRemaining - itemsAdded);
            }
            Console.WriteLine(menu.menu + " takes " + rounds+" to cook");
            
        }
    
    }

    static int AddToGrill(List<MenuItem> items, Grill grill)
        {
            int count = 0;
            foreach (var item in items.ToList<MenuItem>())
            { 
                
                var spaceOnGrill = FindSpaceOnGrill(grill, item.Width, item.Length);
                if (spaceOnGrill != null)
                { 
                    item.position = SplitGrillSpace(spaceOnGrill, item.Width, item.Length);
                    count++;
                    totalItems.Remove(item);
                }
            }
            return count;
        }

        static Grill FindSpaceOnGrill(Grill grill, int itemWidth, int itemLength)
        {
            if (grill.isOccupied) 
            {
                var nextSpace = FindSpaceOnGrill(grill.bottomCorner, itemWidth, itemLength);

                if (nextSpace == null)
                {
                    nextSpace = FindSpaceOnGrill(grill.rightCorner, itemWidth, itemLength);
                }

                return nextSpace;
            }
            else if ((itemWidth <= grill.Width && itemLength <= grill.Length) || (itemWidth <= grill.Length && itemLength <= grill.Width))
            {
                return grill;
            }
            else 
            {
                return null;
            }
        }

        static Grill SplitGrillSpace(Grill grill, int itemWidth, int itemLength)
        {
            grill.isOccupied = true;
            grill.bottomCorner = new Grill { pos_z = grill.pos_z, pos_x = grill.pos_x + itemLength,Length = grill.Length, Width = grill.Width - itemWidth };
            grill.rightCorner = new Grill {  pos_z = grill.pos_z + itemLength, pos_x = grill.pos_x,Length = grill.Length - itemLength, Width = itemWidth };
            return grill;
        }

        
    

   
}


