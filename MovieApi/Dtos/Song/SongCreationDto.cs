using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Movie
{
    public class SongCreationDto
    {
        [Required(ErrorMessage = "Movie Title is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Title length is 50 characters")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Movie Duration is required.")]
        [Range(20, 1000, ErrorMessage = "Movie Duration must be between 20 and 1000.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Movie Release Date is required.")]
        public string? ReleaseDate { get; set; }
    }
}
