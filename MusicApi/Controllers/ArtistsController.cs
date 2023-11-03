using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Dtos;
using MusicApi.Helpers;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;
        public ArtistsController(ApiDbContext dbContext) 
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
        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await _dbContext.Artists.ToListAsync();

            var artistsWithImages = await Task.WhenAll(artists.Select(async x => new
            {
                Id = x.Id,
                Name = x.Name,
                ProfilePicture = await FileHelper.GetImageByImageIdAsync(x.ProfileImageId)
            }));

            return Ok(artistsWithImages);
        }
    }
}
