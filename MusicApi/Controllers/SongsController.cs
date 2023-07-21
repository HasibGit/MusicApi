using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private static List<Song> songs = new List<Song>()
        {
            new Song(){ Id = "gk123", Title = "Forever", Language = "English"},
            new Song() { Id = "tv351", Title = "Someone Special", Language = "Korean" }
        };

        [HttpGet]
        public IEnumerable<Song> get()
        {
            return songs;
        }

        [HttpPost]
        public void post([FromBody]Song song)
        {
            songs.Add(song);
        }

        [HttpPut("{id}")]
        public void put(string id, [FromBody]Song song)
        {
            int index = songs.FindIndex(song => song.Id == id);

            songs[index] = song;
        }

        [HttpDelete("{id}")]
        public void delete(string id)
        {
            int index = songs.FindIndex(song => song.Id == id);
            songs.RemoveAt(index);
        }
    }
}
