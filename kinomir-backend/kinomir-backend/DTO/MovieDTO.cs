namespace kinomir_backend.DTO;

public class MovieDTO
{
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public string MovieDescription { get; set; }
    public string MoviePosterVertical { get; set; }
    public string MoviePosterHorizontal { get; set; }
    public short? MovieReleaseYear { get; set; }
    public short? MovieDuration { get; set; }
    public string MovieAgeRaiting { get; set; }
    public List<string> Tags { get; set; }
}