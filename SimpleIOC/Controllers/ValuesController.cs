using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleIOC.Model;
using SimpleIOC.Model.IOC;

namespace SimpleIOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            IOCFactory f = new IOCFactory();
             f.Load("SimpleIOC");
            Student student = (Student)f.Getobj("Student");
            return student.Studeny();

        }
    }
}
