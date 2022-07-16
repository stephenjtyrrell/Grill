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
        List<Menu> sortedMenus = getMenus().OrderBy(m=>m.menu).ToList();
        foreach(Menu fullMenu in sortedMenus){
            List<MenuItem> menuItems = fullMenu.items;
            Console.WriteLine(fullMenu.menu + " has " + menuItems.Count + " types of items");
            int quantityOfItems = new int();
            int totalSurfaceAreaFood = new int();
            foreach(MenuItem menuItem in menuItems){
                Console.WriteLine(menuItem.Name + " has a quantity of " + menuItem.Quantity);
                quantityOfItems = quantityOfItems + menuItem.Quantity;
                menuItem.SurfaceArea = menuItem.Length * menuItem.Width;
                totalSurfaceAreaFood = totalSurfaceAreaFood + (menuItem.SurfaceArea * menuItem.Quantity);
            }
            Console.WriteLine(fullMenu.menu + " has " + quantityOfItems + " total items with a total surface area of " + totalSurfaceAreaFood);

        }

    }

    static List<Menu> getMenus()
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
        return Menus;
    }
}


