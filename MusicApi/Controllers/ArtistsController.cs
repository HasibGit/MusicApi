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
        public async Task<IActionResult> GetArtists(int pageNumber = 0, int pageSize = 5)
        {
            var artists = await _dbContext.Artists.Select(x => new
            {
               x.Id,
               x.Name,
               x.ProfileImageId
            }).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

            return Ok(artists);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetArtistDetails(Guid artistId)
        {
            var artistDetails = await _dbContext.Artists.Where(x => x.Id == artistId).Include(x => x.Songs).FirstOrDefaultAsync();

            return Ok(artistDetails);
        }
       
    }
}
