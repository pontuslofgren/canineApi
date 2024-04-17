using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using canineApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors

namespace canineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DogsController : ControllerBase
{

    private readonly CanineContext _context;
    private readonly string _blobConnectionString;

    public DogsController(IConfiguration config, CanineContext context)
    {
        _context = context;
        _blobConnectionString = config.GetConnectionString("BlobStorage")!;
    }
 [HttpGet]
 public async Task<ActionResult<List<Dog>>> Get()
 {
    var dogs = await _context.Dogs.ToListAsync();
    return Ok(dogs);
 }

 [HttpPost]
 [EnableCors("OpenPolicy")]
 public async Task<ActionResult<Dog>> Post([FromForm] DogRequest request)
 {
    var blobServiceClient = new BlobServiceClient(_blobConnectionString);
   
   BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("dogblobs");
   
   // Set filename for blob
   string fileName = request.Name + "_" + Guid.NewGuid().ToString();
// Get a reference to a blob
   var blobClient = containerClient.GetBlobClient(fileName);
   
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

}
