namespace MovieApi.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
