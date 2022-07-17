using GrillSolution;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

    class Program
{
    static void Main(string[] args)
    {
        int grillLength = 20;
        int grillWidth = 30;
        int grillSurfaceArea = grillLength * grillWidth;
        List<Menu> sortedMenus = GetMenus().OrderBy(m=>m.menu).ToList();
        //Assuming we can cut up the food and maximise the space used on the grill, filling every cm of the grill
        CutUpFoodAndCalculateRounds(grillSurfaceArea, sortedMenus);
        //Without cutting up the food
        CalculateRoundsWithoutCuttingFood(grillLength,grillWidth, sortedMenus);
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
                menuItem.SurfaceArea = menuItem.Length * menuItem.Width;
                //add the total surface area of all of these items to the total for the menu
                totalSurfaceAreaFood = totalSurfaceAreaFood + (menuItem.SurfaceArea * menuItem.Quantity);
            }
            //calculate the rounds needed by dividing the total surface area of the food by the surface area of the grill
            decimal roundDecimals = Decimal.Divide(totalSurfaceAreaFood,grillSurfaceArea);
            //round up to nearest integer
            var rounds = Math.Ceiling(roundDecimals);
            Console.WriteLine(fullMenu.menu + " will take " + rounds + " rounds, if we cut up the food");

        }
    }

    static void CalculateRoundsWithoutCuttingFood(int grillLength, int grillWidth, List<Menu>Menus)
    {
        foreach(Menu fullMenu in Menus){
            //order menuItems by quantity descending
            List<MenuItem> sortedByQuantity = fullMenu.items.OrderBy(m=>m.Quantity).ToList();
            foreach(MenuItem menuItem in sortedByQuantity){
                Console.WriteLine(fullMenu.menu + ", " + menuItem.Name + ", " + menuItem.Width + ", " + menuItem.Length + ", " + menuItem.Quantity);
                
            }
        }
    }
}


