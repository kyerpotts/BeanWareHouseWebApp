using BeanWareHouseWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using FALib2;
using RestSharp;
using Newtonsoft.Json;

namespace BeanWareHouseWebApp.Controllers
{
    public class SearchBeansController : Controller
    {
        RestClient client = new RestClient("http://localhost:5025/beanwarehouse/");
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("SearchBeans/{type}/{value}")]
        public IActionResult SearchBeans(string type, string value)
        {
            //List<Bean>? beanList = null;
            RestRequest request = new RestRequest("GetByValue/{type}/{value}", Method.Get)
                .AddUrlSegment("type", type)
                .AddUrlSegment("value", value);
            String? responseContent = null;

            RestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                if (response.Content != null)
                {
                    responseContent = JsonConvert.DeserializeObject<String>(response.Content);
                }
                return Ok(responseContent);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
