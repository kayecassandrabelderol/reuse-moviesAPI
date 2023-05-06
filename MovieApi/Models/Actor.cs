using Microsoft.VisualBasic;

namespace MovieApi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthday { get; set; }
        public List<Song> Movies { get; set; } = new List<Song>();
    }
}
