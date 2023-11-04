using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 0, int pageSize = 5)
        {
            var songs = await _dbContext.Songs.Select(
                                    x => new { 
                                        x.Id, 
                                        x.Title, 
                                        x.Duration, 
                                        x.ImageId, 
                                        x.AudioFileId 
                                    }).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FeaturedSongs()
        {
            var featuredSongs = await _dbContext.Songs
                .Where(x => x.IsFeatured == true)
                .Select(x => new {x.Id, x.Title, x.Duration, x.ImageId, x.AudioFileId})
                .ToListAsync();
            return Ok(featuredSongs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var newSongs = await _dbContext.Songs
                .OrderBy(x => x.UploadDate)
                .Select(x => new { x.Id, x.Title, x.Duration, x.ImageId, x.AudioFileId })
                .Take(15)
                .ToListAsync();
            return Ok(newSongs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(string searchKey)
        {
            var result = await _dbContext.Songs
                .Where(x => x.Title.ToLower().Contains(searchKey.ToLower()))
                .OrderBy(x => x.UploadDate)
                .Select(x => new { x.Id, x.Title, x.Duration, x.ImageId, x.AudioFileId })
                .ToListAsync();
            return Ok(result);
        }
    }
}
