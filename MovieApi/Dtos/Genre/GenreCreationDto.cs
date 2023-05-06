using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Genre
{
    public class GenreCreationDto
    {
        [Required(ErrorMessage = "Genre Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Name length is 50 characters")]
        public string? Name { get; set; }
    }
}
