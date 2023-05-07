using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Movie
{
    public class SongUpdateDto
    {
        [Required(ErrorMessage = "Song Release Date is required.")]
        public string? ReleaseDate { get; set; }
    }
}
