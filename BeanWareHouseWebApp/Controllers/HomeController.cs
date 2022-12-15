using FALib2;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace BeanWareHouseWebApp.Controllers
{
    public class HomeController : Controller
    {
        RestClient client = new RestClient("http://localhost:5025/beanwarehouse/");
        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            List<Bean>? beanList = null;
            RestRequest request = new RestRequest("GetAllBeans", Method.Get);
            RestResponse response = client.Execute(request);
            if (response.Content != null)
            {
                String? responseContent = JsonConvert.DeserializeObject<String>(response.Content);
                if (responseContent != null)
                {
                    beanList = JsonConvert.DeserializeObject<List<Bean>>(responseContent);
                }
            }
            return View(beanList);
        }
    }
}
