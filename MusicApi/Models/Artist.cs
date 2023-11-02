using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Models
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public string ProfileImageId { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
