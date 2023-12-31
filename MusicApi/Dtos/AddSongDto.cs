﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Dtos
{
    public class AddSongDto
    {
        public string Title { get; set; }
        public string Duration { get; set; }
        public bool IsFeatured { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile AudioFile { get; set; }
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }
    }
}
