using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Actor
{
    public class ArtistUpdateDto
    {
        [Required(ErrorMessage = "Artist Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Name length is 50 characters")]
        public string? Name { get; set; }
    }
}
