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
    public class SongsController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public SongsController(ApiDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        // POST api/<SongsController>
        [HttpPost("AddSong")]
        public async Task<IActionResult> Post([FromForm] AddSongDto songDto)
        {
            var song = new Song
            {
                Title = songDto.Title,
                Duration = songDto.Duration,
                UploadDate = DateTime.UtcNow,
                IsFeatured = songDto.IsFeatured,
                Image = songDto.Image,
                AudioFile = songDto.AudioFile,
                ArtistId = songDto.ArtistId,
                AlbumId = songDto.AlbumId
            };

            song.ImageId = await FileHelper.UploadFile(songDto.Image);
            song.AudioFileId = await FileHelper.UploadFile(songDto.AudioFile);

            if (string.IsNullOrEmpty(song.ImageId) || string.IsNullOrEmpty(song.AudioFileId))
            {
                return BadRequest();
            }

            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
