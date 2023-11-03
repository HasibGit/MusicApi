namespace MusicApi.Dtos
{
    public class AddAlbumDto
    {
        public string Name { get; set; }
        public IFormFile CoverImage { get; set; }
        public Guid ArtistId { get; set; }
    }
}
