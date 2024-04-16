using Microsoft.AspNetCore.Mvc;

namespace canineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DogsController : ControllerBase
{
 [HttpGet]
 public string Get()
 {
    return "Hello";
 } 
}
