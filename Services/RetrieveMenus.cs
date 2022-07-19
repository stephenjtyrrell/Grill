using Grill.DataModel;
using Newtonsoft.Json;
using RestSharp;

namespace Grill{
    class RetrieveMenus{

    public static List<Menu> GetMenus()
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
    }
}