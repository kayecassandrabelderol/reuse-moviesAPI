namespace MovieApi.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Song> Movies { get; set; } = new List<Song>();
    }
}
