namespace MovieApi.Dtos.Movie
{
    public class SongByIdDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Duration { get; set; }
        public string? ReleaseDate { get; set; }
        public List<string?> Genres { get; set; } = new List<string?>();
        public List<string?> Actors { get; set; } = new List<string?>();
        public List<string?> Awards { get; set; } = new List<string?>();
    }
}
