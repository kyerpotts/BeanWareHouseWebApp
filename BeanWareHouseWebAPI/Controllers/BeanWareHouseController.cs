using BeanWareHouseWebAPI.Models;
using FALib2;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BeanWareHouseWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeanWareHouseController : Controller
    {
        private readonly BeanWarehouseSingleton _beanWareHouseSingleton;

        public BeanWareHouseController(BeanWarehouseSingleton beanWareHouseSingleton)
        {
            _beanWareHouseSingleton = beanWareHouseSingleton;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetAllBeans")]
        public IActionResult GetAllBeans()
        {
            Bean[] beans = _beanWareHouseSingleton.GetBeans();
            String beansJson = JsonConvert.SerializeObject(beans);

            return Ok(beansJson);
        }

        [HttpGet]
        [Route("GetByValue/{type}/{value}")]
        public IActionResult GetByValue(string type, string value)
        {
            Bean[] beans = _beanWareHouseSingleton.GetBeans();
            List<Bean> beanList = new List<Bean>();
            foreach (Bean bean in beans)
            {
                switch (type)
                {
                    case "size":
                        if (bean.size == value)
                        {
                            beanList.Add(bean);
                        }
                        break;
                    case "colour":
                        if (bean.colour == value)
                        {
                            beanList.Add(bean);
                        }
                        break;
                    case "weight":
                        int weightVal;
                        if (int.TryParse(value, out weightVal))
                        {
                            if (bean.weight == weightVal)
                            {
                                beanList.Add(bean);
                            }
                        }
                        else
                        {
                            return BadRequest();
                        }
                        break;
                    case "price":
                        int priceVal;
                        if (int.TryParse(value, out priceVal))
                        {
                            if (bean.price == priceVal)
                            {
                                beanList.Add(bean);
                            }
                        }
                        else
                        {
                            return BadRequest();
                        }
                        break;
                    default:
                        return BadRequest();

                }
            }

            String jsonBeanList = JsonConvert.SerializeObject(beanList);
            return Ok(jsonBeanList);
        }


        [HttpPost]
        [Route("AddBean/{size}/{colour}/{weight}/{price}")]
        public IActionResult AddBean(string size, string colour, string weight, string price)
        {
            Bean newBean = new Bean();
            int newWeight;
            int newPrice;
            if (!int.TryParse(weight, out newWeight))
            {
                return BadRequest("Weight is invalid. Must be an integer");
            }
            if (!int.TryParse(price, out newPrice))
            {
                return BadRequest("Price is invalid. Must be an integer");
            }

            newBean.size = size;
            newBean.colour = colour;
            newBean.weight = newWeight;
            newBean.price = newPrice;

            try
            {
                _beanWareHouseSingleton.AddBean(newBean);
            }
            catch (InvalidBeanException ex)
            {
                return BadRequest("Bean parameters invalid" + ex.Message);
            }
            return Ok("Successfully Added Bean");
        }

        [HttpDelete]
        [Route("DeleteBean/{index}")]
        public IActionResult DeleteBean(int index)
        {
            try
            {
                _beanWareHouseSingleton.DeleteBean(index);
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Successfully Deleted");
        }

        [HttpPut]
        [Route("UpdateBean/{index}/{size}/{colour}/{weight}/{price}")]
        public IActionResult UpdateBean(int index, string size, string colour, string weight, string price)
        {
            int newWeight;
            int newPrice;

            if (!int.TryParse(weight, out newWeight))
            {
                return BadRequest("Weight is invalid. Must be an integer");
            }
            if (!int.TryParse(price, out newPrice))
            {
                return BadRequest("Price is invalid. Must be an integer");
            }

            Bean bean = new Bean();
            bean.size = size;
            bean.colour = colour;
            bean.weight = newWeight;
            bean.price = newPrice;

            try
            {
                _beanWareHouseSingleton.ReplaceBean(bean, index);
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidBeanException ex)
            {
                return BadRequest("Bean parameters invalid" + ex.Message);
            }

            return Ok("Bean at index: " + index + " updated");
        }



    }
}
