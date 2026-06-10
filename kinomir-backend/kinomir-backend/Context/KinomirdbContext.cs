using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using kinomir_backend.Models;

namespace kinomir_backend.Context;

public partial class KinomirdbContext : DbContext
{
    public KinomirdbContext()
    {
    }

    public KinomirdbContext(DbContextOptions<KinomirdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgeRaiting> AgeRaitings { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<InTheater> InTheaters { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Theater> Theaters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=kinomirdb; Username=admin; Password=adm1n");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgeRaiting>(entity =>
        {
            entity.HasKey(e => e.AgeRaitingId).HasName("age_raiting_pk");

            entity.ToTable("age_raiting");

            entity.Property(e => e.AgeRaitingId)
                .ValueGeneratedNever()
                .HasColumnName("age_raiting_id");
            entity.Property(e => e.AgeRaitingName)
                .HasColumnType("character varying")
                .HasColumnName("age_raiting_name");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("bookings_pk");

            entity.ToTable("bookings");

            entity.Property(e => e.BookingId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("booking_id");
            entity.Property(e => e.RowNumber).HasColumnName("row_number");
            entity.Property(e => e.SeatNumber).HasColumnName("seat_number");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UserEmail)
                .HasColumnType("character varying")
                .HasColumnName("user_email");
            entity.Property(e => e.UserPhone)
                .HasColumnType("character varying")
                .HasColumnName("user_phone");

            entity.HasOne(d => d.Session).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("bookings_sessions_fk");
        });

        modelBuilder.Entity<InTheater>(entity =>
        {
            entity.HasKey(e => e.InTheatersId).HasName("in_theaters_pk");

            entity.ToTable("in_theaters");

            entity.Property(e => e.InTheatersId)
                .ValueGeneratedNever()
                .HasColumnName("in_theaters_id");
            entity.Property(e => e.InTheatersValue).HasColumnName("in_theaters_value");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("movies_pk");

            entity.ToTable("movies");

            entity.Property(e => e.MovieId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("movie_id");
            entity.Property(e => e.MovieAgeRaitingId).HasColumnName("movie_age_raiting_id");
            entity.Property(e => e.MovieDescription)
                .HasColumnType("character varying")
                .HasColumnName("movie_description");
            entity.Property(e => e.MovieDirector)
                .HasColumnType("character varying")
                .HasColumnName("movie_director");
            entity.Property(e => e.MovieDuration).HasColumnName("movie_duration");
            entity.Property(e => e.MovieInTheatersId).HasColumnName("movie_in_theaters_id");
            entity.Property(e => e.MoviePosterHorizontal)
                .HasColumnType("character varying")
                .HasColumnName("movie_poster_horizontal");
            entity.Property(e => e.MoviePosterVertical)
                .HasColumnType("character varying")
                .HasColumnName("movie_poster_vertical");
            entity.Property(e => e.MovieReleaseYear).HasColumnName("movie_release_year");
            entity.Property(e => e.MovieTitle)
                .HasColumnType("character varying")
                .HasColumnName("movie_title");

            entity.HasOne(d => d.MovieAgeRaiting).WithMany(p => p.Movies)
                .HasForeignKey(d => d.MovieAgeRaitingId)
                .HasConstraintName("movies_age_raiting_fk");

            entity.HasOne(d => d.MovieInTheaters).WithMany(p => p.Movies)
                .HasForeignKey(d => d.MovieInTheatersId)
                .HasConstraintName("movies_in_theaters_fk");

            entity.HasMany(d => d.Tags).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("movie_tags_tags_fk"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("movie_tags_movies_fk"),
                    j =>
                    {
                        j.HasKey("MovieId", "TagId").HasName("movie_tags_pk");
                        j.ToTable("movie_tags");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("news_pk");

            entity.ToTable("news");

            entity.Property(e => e.NewsId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("news_id");
            entity.Property(e => e.NewsContent)
                .HasColumnType("character varying")
                .HasColumnName("news_content");
            entity.Property(e => e.NewsTitle)
                .HasColumnType("character varying")
                .HasColumnName("news_title");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("promotions_pk");

            entity.ToTable("promotions");

            entity.Property(e => e.PromotionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("promotion_id");
            entity.Property(e => e.PromotionContent)
                .HasColumnType("character varying")
                .HasColumnName("promotion_content");
            entity.Property(e => e.PromotionTitle)
                .HasColumnType("character varying")
                .HasColumnName("promotion_title");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("sessions_pk");

            entity.ToTable("sessions");

            entity.Property(e => e.SessionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("session_id");
            entity.Property(e => e.SessionDate).HasColumnName("session_date");
            entity.Property(e => e.SessionMovieId).HasColumnName("session_movie_id");
            entity.Property(e => e.SessionTheater).HasColumnName("session_theater");
            entity.Property(e => e.SessionTheaterHall)
                .HasColumnType("character varying")
                .HasColumnName("session_theater_hall");
            entity.Property(e => e.SessionTime).HasColumnName("session_time");
            entity.Property(e => e.SessionsPrice).HasColumnName("sessions_price");

            entity.HasOne(d => d.SessionMovie).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.SessionMovieId)
                .HasConstraintName("sessions_movies_fk");

            entity.HasOne(d => d.SessionTheaterNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.SessionTheater)
                .HasConstraintName("sessions_theaters_fk");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("tags_pk");

            entity.ToTable("tags");

            entity.Property(e => e.TagId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tag_id");
            entity.Property(e => e.TagName)
                .HasColumnType("character varying")
                .HasColumnName("tag_name");
        });

        modelBuilder.Entity<Theater>(entity =>
        {
            entity.HasKey(e => e.TheaterId).HasName("theaters_pk");

            entity.ToTable("theaters");

            entity.Property(e => e.TheaterId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("theater_id");
            entity.Property(e => e.TheaterAddress)
                .HasColumnType("character varying")
                .HasColumnName("theater_address");
            entity.Property(e => e.TheaterName)
                .HasColumnType("character varying")
                .HasColumnName("theater_name");
            entity.Property(e => e.TheaterPlace)
                .HasColumnType("character varying")
                .HasColumnName("theater_place");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
