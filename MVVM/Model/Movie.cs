namespace MovieApp.MVVM.Model
{
    internal class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string OriginalTitle { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public double Popularity { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public GenreList Genres { get; set; } = new GenreList();
        public List<ProductionCompany> ProductionCompanies { get; set; } = new List<ProductionCompany>(); 
        public List<Actor> Cast { get; set; } = new List<Actor>(); 
        public List<MovieProvider> Providers { get; set; } = new List<MovieProvider>(); 
    }
}
