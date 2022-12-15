using FALib2;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace BeanWareHouseWebApp.Controllers
{
    public class AddBeansController : Controller
    {
        RestClient client = new RestClient("http://localhost:5025/beanwarehouse/");

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("AddBean/{size}/{colour}/{weight}/{price}")]
        public IActionResult AddBean(string size, string colour, string weight, string price)
        {
            RestRequest request = new RestRequest("AddBean/{size}/{colour}/{weight}/{price}", Method.Post)
                .AddUrlSegment("size", size)
                .AddUrlSegment("colour", colour)
                .AddUrlSegment("weight", weight)
                .AddUrlSegment("price", price);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Bean Added");

            }
            else
            {
                var errMsg = response.ErrorMessage;
                return BadRequest(errMsg);
            }

        }
    }
}
