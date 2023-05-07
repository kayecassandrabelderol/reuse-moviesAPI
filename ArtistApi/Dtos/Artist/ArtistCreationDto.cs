using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos.Actor
{
    public class ArtistCreationDto
    {
        [Required(ErrorMessage = "Artist Name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum Name length is 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Artist Gender is required.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Artist Birthday is required.")]
        public string? Birthday { get; set; }
    }
}
