﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Dtos;
using MusicApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public SongsController(ApiDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Songs.ToListAsync());
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return BadRequest("Invalid ID format");
            }

            Song? song = await _dbContext.Songs.FindAsync(guidId);

            if (song == null)
            {
                return NotFound("Resource not found");
            }

            return Ok(song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddSongDto songDto)
        {

            var song = new Song
            {
                Id = Guid.NewGuid(),
                Title = songDto.Title,
                Language = songDto.Language,
                Duration = songDto.Duration,
                Image = songDto.Image
            };

            song.ImageId = await WriteFile(song.Image);

            if (string.IsNullOrEmpty(song.ImageId))
            {
                return BadRequest();
            }

            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            string fileId = "";
            try
            {
                var extension = "." + file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
                fileName = Guid.NewGuid().ToString() + extension;

                fileId = fileName.Substring(0, fileName.Length - extension.Length);

                var rootDirectory = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(rootDirectory, "Upload\\Files");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Console.WriteLine("Directory created successfully");
                }

                var exactPath = Path.Combine(filePath, fileName);

                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                    return fileId;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return fileId;
            }
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Song songObj)
        {

            Guid songId;
            if (!Guid.TryParse(id, out songId))
            {
                // Handle invalid Guid format or error
                // For example, you can return a BadRequest response here.
                // return BadRequest("Invalid ID format.");
                return BadRequest();
            }

            var song = await _dbContext.Songs.FindAsync(songId);

            if(song == null)
            {
                return NotFound("Resource not found");
            }

            song.Title = songObj.Title;
            song.Language = songObj.Language;
            song.Duration = songObj.Duration;
            await _dbContext.SaveChangesAsync();

            return Ok("record updated successfully");
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Guid songId;
            if (!Guid.TryParse(id, out songId))
            {
                // Handle invalid Guid format or error
                // For example, you can return a BadRequest response here.
                // return BadRequest("Invalid ID format.");
                return BadRequest();
            }
            var song = await _dbContext.Songs.FindAsync(songId);

            if(song == null)
            {
                return NotFound("Resource not found");
            }

            _dbContext.Songs.Remove(song);
            await _dbContext.SaveChangesAsync();
            return Ok("record deleted successfully");
        }
    }
}
