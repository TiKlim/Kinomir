using System;
using System.Collections.Generic;

namespace kinomir_backend.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? MovieTitle { get; set; }

    public int? MovieAgeRaitingId { get; set; }

    public string? MovieDescription { get; set; }

    public string? MoviePosterVertical { get; set; }

    public string? MoviePosterHorizontal { get; set; }

    public string? MovieDirector { get; set; }

    public short? MovieReleaseYear { get; set; }

    public int? MovieInTheatersId { get; set; }

    public short? MovieDuration { get; set; }

    public virtual AgeRaiting? MovieAgeRaiting { get; set; }

    public virtual InTheater? MovieInTheaters { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
