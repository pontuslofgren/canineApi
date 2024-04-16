using canineApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace canineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DogsController : ControllerBase
{

    private readonly CanineContext _context;

    public DogsController(CanineContext context)
    {
        _context = context;
    }
 [HttpGet]
 public async Task<ActionResult<List<Dog>>> Get()
 {
    var dogs = await _context.Dogs.ToListAsync();
    return Ok(dogs);
 }

 [HttpPost]
 public async Task<ActionResult<Dog>> Post(Dog dog)
 {
    _context.Dogs.Add(dog);
    await _context.SaveChangesAsync();
    return Ok(dog);
 }

}
