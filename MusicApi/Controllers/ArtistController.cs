using Microsoft.AspNetCore.Mvc;
using MusicApi.Data;
using MusicApi.Dtos;
using MusicApi.Helpers;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        public ArtistController(ApiDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        // POST api/<ArtistsController>
        [HttpPost("AddArtist")]
        public async Task<IActionResult> Post([FromForm] AddArtistDto artistDto)
        {
            var artist = new Artist
            {
                Name = artistDto.Name,
                Gender = artistDto.Gender,
                ProfileImage = artistDto.ProfileImage
            };

            artist.ProfileImageId = await FileHelper.UploadFile(artistDto.ProfileImage);

            if (string.IsNullOrEmpty(artist.ProfileImageId))
            {
                return BadRequest();
            }

            await _dbContext.Artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
