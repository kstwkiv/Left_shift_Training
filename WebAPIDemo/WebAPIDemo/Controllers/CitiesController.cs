using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Model;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        public static List<string> cityList = null;
        public CitiesController()
        {
            if (cityList == null)
            {
                cityList = new List<string>()
                {
                    "Delhi",
                    "Pune",
                    "Hyderabad",
                    "Mumbai",
                    "Ahmedabad",
                    "Bengaluru/Banglore"
                };
            }
        }
        [Route("JoiningCities")]
        //[Route("/Cg")]
        [HttpGet]
        public List<string> ShowAllCities()
        {
            return cityList;
        }
        [Route("GetCityList/{stateName}")]
        [HttpGet]
        public List<string> GetCityList(string stateName) {
            return cityList;
        }
        [Route("FetchAllCities/{stateID}")]
        [HttpGet]
        public List<string> FetchAllCities(int stateID)
        {
            return cityList;
        }
        [HttpGet]
        public int AddMe(int n1,int n2)
        {
            return n1 + n2;
        }
        [Route("DoSomeTask1")]
        [HttpPost]
        public ActionResult DoSomeTask1(int empID,[FromBody]string Name)
        {
            return Created();
                   //Ok(new {empID,Name});

        }
        [Route("DoSomeTask2")]
        [HttpPost]
        public void DoSomeTask2([FromQuery] Student Sobj)
        {

        }
    }
}
