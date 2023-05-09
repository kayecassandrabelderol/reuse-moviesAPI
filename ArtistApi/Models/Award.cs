namespace ArtistApi.Models
{
    public class Award
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public int SongId { get; set; }
    }
}
