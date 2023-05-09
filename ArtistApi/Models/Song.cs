namespace ArtistApi.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Artist> Artists { get; set; } = new List<Artist>();
        public List<Award> Awards { get; set; } = new List<Award>();

    }
}
