using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Models
{
    public class Album
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public IFormFile CoverImage { get; set; }
        public string CoverImageId { get; set; }
        public Guid ArtistId { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
