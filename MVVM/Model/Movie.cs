using System.ComponentModel.DataAnnotations;

namespace MovieApp.MVVM.Model
{
    internal class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public double Popularity { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public GenreList Genres { get; set; } = new GenreList();
    }
}
