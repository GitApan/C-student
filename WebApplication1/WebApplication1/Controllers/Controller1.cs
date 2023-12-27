using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

namespace WebApplication1.Controllers;

[Route("api")]
[ApiController]
public class Controller1 : Controller
{
    private readonly IMyService _myService;

    public Controller1(IMyService myService)
    {
        _myService = myService;
    }

    [HttpGet]
    public void Index()
    {
        _myService.DoSomething();
        
    }
}