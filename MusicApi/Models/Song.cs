using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Models
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsFeatured { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImageId { get; set; }
        [NotMapped]
        public IFormFile AudioFile { get; set; }
        public string AudioFileId { get; set; }
        public Guid ArtistId { get; set; }
        public Guid? AlbumId { get; set; }
    }
}
