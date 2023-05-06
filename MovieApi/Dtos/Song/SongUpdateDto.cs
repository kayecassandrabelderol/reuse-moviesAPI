using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Movie
{
    public class SongUpdateDto
    {
        [Required(ErrorMessage = "Movie Release Date is required.")]
        public string? ReleaseDate { get; set; }
    }
}
