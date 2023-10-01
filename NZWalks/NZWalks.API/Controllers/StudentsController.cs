using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    //https: //localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //get : https: //localhost:portnumber/api/students

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentName =new string[] {"sayed" , "jane" , "Mark", "emily", "David"};
       return Ok(studentName);
        }


    }
}
