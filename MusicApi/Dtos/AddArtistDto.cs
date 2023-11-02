using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Dtos
{
    public class AddArtistDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
