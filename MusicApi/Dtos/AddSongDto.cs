using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Dtos
{
    public class AddSongDto
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public IFormFile Image { get; set; }
    }
}
