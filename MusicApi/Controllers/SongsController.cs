using Microsoft.AspNetCore.Mvc;
using MusicApi.Data;
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
        public IEnumerable<Song> Get()
        {
            return _dbContext.Songs;
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public Song Get(string id)
        {
            Song song = _dbContext.Songs.Find(id);
            return song;
        }

        // POST api/<SongsController>
        [HttpPost]
        public void Post([FromBody] Song song)
        {
            song.Id = Guid.NewGuid();
            _dbContext.Songs.Add(song);
            _dbContext.SaveChanges();
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Song songObj)
        {

            Guid songId;
            if (!Guid.TryParse(id, out songId))
            {
                // Handle invalid Guid format or error
                // For example, you can return a BadRequest response here.
                // return BadRequest("Invalid ID format.");
                return;
            }

            var song = _dbContext.Songs.Find(songId);

            song.Title = songObj.Title;
            song.Language = songObj.Language;
            _dbContext.SaveChanges();
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Guid songId;
            if (!Guid.TryParse(id, out songId))
            {
                // Handle invalid Guid format or error
                // For example, you can return a BadRequest response here.
                // return BadRequest("Invalid ID format.");
                return;
            }
            var song = _dbContext.Songs.Find(songId);
            _dbContext.Songs.Remove(song);
            _dbContext.SaveChanges();
        }
    }
}
