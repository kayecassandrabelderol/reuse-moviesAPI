using System.ComponentModel.DataAnnotations;

namespace ArtistApi.Dtos.Movie
{
    public class SongUpdateDto
    {
        [Required(ErrorMessage = "Song Release Date is required.")]
        public string? ReleaseDate { get; set; }
    }
}
