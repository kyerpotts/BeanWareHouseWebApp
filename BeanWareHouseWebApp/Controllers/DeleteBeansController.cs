using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace BeanWareHouseWebApp.Controllers
{
    public class DeleteBeansController : Controller
    {
        RestClient client = new RestClient("http://localhost:5025/beanwarehouse/");
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        [Route("DeleteBean/{index}")]
        public IActionResult DeleteBean(int index)
        {
            RestRequest request = new RestRequest("DeleteBean/{index}", Method.Delete)
                .AddUrlSegment("index", index);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Bean Deleted");
            }
            else
            {
                var errMsg = response.ErrorMessage;
                return BadRequest(errMsg);
            }

        }
    }
}
