using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using canineApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace canineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DogsController : ControllerBase
{

    private readonly CanineContext _context;
    private readonly string _blobConnectionString;
    private readonly BlobServiceClient _blobServiceClient;

    private readonly BlobContainerClient _containerClient;

    public DogsController(IConfiguration config, CanineContext context)
    {
        _context = context;
        _blobConnectionString = config.GetConnectionString("BlobStorage")!;
        _blobServiceClient = new BlobServiceClient(_blobConnectionString);
        _containerClient = _blobServiceClient.GetBlobContainerClient("dogblobs");

    }
 [HttpGet]
 public async Task<ActionResult<List<Dog>>> Get()
 {
    var dogs = await _context.Dogs.ToListAsync();
    return Ok(dogs);
 }

 [HttpPost]
 public async Task<ActionResult<Dog>> Post([FromForm] DogRequest request)
 {
   // Set filename for blob
   string fileName = request.Name + "_" + Guid.NewGuid().ToString();
// Get a reference to a blob
   var blobClient = _containerClient.GetBlobClient(fileName);
   
   using (var stream = request.Image.OpenReadStream())
   {
      await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = request.Image.ContentType}, conditions: null);
   }

   var imageResponseUrl = blobClient.Uri.ToString();

   var newDog = new Dog {
      Name = request.Name,
      ImageUrl = imageResponseUrl
   };

   await _context.Dogs.AddAsync(newDog);
   await _context.SaveChangesAsync();
    return Ok(newDog);
 }

    // DELETE: api/Dog/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDog(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog == null)
        {
            return NotFound();
        }

        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

   //       // PUT: api/Dog/5
   //  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
   //  [HttpPut]
   //  public async Task<IActionResult> PutDog(DogRequest request)
   //  {
   //    var existing = await _context.Dogs.FirstOrDefaultAsync(i => i.Name == request.Name);
   //      if (existing == null)
   //      {
   //          return NotFound();
   //      }

   //    //   _context.Entry(dog).State = EntityState.Modified;

   //  }



}
