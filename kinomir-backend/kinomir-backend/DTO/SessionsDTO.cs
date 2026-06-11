namespace kinomir_backend.DTO;

public class SessionsDTO
{
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public string MoviePosterVertical { get; set; }
    public string MovieAgeRaiting { get; set; }
    public Dictionary<string, List<SessionTimeWithId>> SessionsByDay { get; set; }
}

public class SessionTimeWithId
{
    public string Time { get; set; }
    public int SessionId { get; set; }
}