using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace BeanWareHouseWebApp.Controllers
{
    public class EditBeansController : Controller
    {
        RestClient client = new RestClient("http://localhost:5025/beanwarehouse/");
        public IActionResult Index()
        {
            return View();
        }

        [HttpPut]
        [Route("EditBean/{index}/{size}/{colour}/{weight}/{price}")]
        public IActionResult EditBean(int index, string size, string colour, string weight, string price)
        {
            RestRequest request = new RestRequest("UpdateBean/{index}/{size}/{colour}/{weight}/{price}", Method.Put)
                .AddUrlSegment("index", index)
                .AddUrlSegment("size", size)
                .AddUrlSegment("colour", colour)
                .AddUrlSegment("weight", weight)
                .AddUrlSegment("price", price);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Bean Updated");
            }
            else
            {
                var errMsg = response.ErrorMessage;
                return BadRequest(errMsg);
            }

        }
    }
}
