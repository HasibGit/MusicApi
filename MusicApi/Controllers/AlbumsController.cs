using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Data;
using MusicApi.Dtos;
using MusicApi.Helpers;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public AlbumsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        // POST api/<AlbumsController>
        [HttpPost("AddAlbum")]
        public async Task<IActionResult> Post([FromForm] AddAlbumDto albumDto)
        {
            var album = new Album
            {
                Name = albumDto.Name,
                CoverImage = albumDto.CoverImage,
                ArtistId = albumDto.ArtistId
            };

            album.CoverImageId = await FileHelper.UploadFile(albumDto.CoverImage);

            if (string.IsNullOrEmpty(album.CoverImageId))
            {
                return BadRequest();
            }

            await _dbContext.Albums.AddAsync(album);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
