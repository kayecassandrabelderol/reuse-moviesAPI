namespace MovieApi.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Actor> Actors { get; set; } = new List<Actor>();
        public List<Award> Awards { get; set; } = new List<Award>();

    }
}
