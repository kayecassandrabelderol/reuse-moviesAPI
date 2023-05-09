using Microsoft.VisualBasic;

namespace ArtistApi.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthday { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
