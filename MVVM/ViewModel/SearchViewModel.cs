using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;

namespace MovieApp.MVVM.ViewModel
{
    internal class SearchViewModel : ViewModelBase
    {
		private MovieContext movieContext;
		private CastContext castContext;
		private MovieProviderContext movieProviderContext;

		private Movie? selectedMovie;

		public Movie? SelectedMovie 
		{
			get { return selectedMovie; }
			set
			{
				selectedMovie = value;
				OnPropertyChanged();
			}
		}

		public SearchViewModel()
		{
			movieContext = new MovieContext();
			castContext = new CastContext();
			movieProviderContext = new MovieProviderContext();

            InitializeMovie();
        }

        public SearchViewModel(string movieName)
		{
			movieContext = new MovieContext();
			castContext = new CastContext();
			movieProviderContext = new MovieProviderContext();

			InitializeMovie(movieName).GetAwaiter();
		}

		private async Task InitializeMovie()
		{
            var popularMovies = await movieContext.GetPopularMovies();

            Movie? movie = await movieContext.GetMovieById(popularMovies.First().Id);

            if (movie != null)
            {
                movie.Cast = await castContext.GetCastByMovieId(movie.Id);
                movie.Providers = await movieProviderContext.GetProvidersByMovieId(movie.Id);
                SelectedMovie = movie;
            }
        }

		private async Task InitializeMovie(string movieName)
		{
			int movieId = await movieContext.GetMovieId(movieName);

			if (movieId != 0)
			{
                Movie? movie = await movieContext.GetMovieById(movieId);

                if (movie != null)
                {
                    movie.Cast = await castContext.GetCastByMovieId(movieId);
                    movie.Providers = await movieProviderContext.GetProvidersByMovieId(movieId);
                    SelectedMovie = movie;
                }
			}
		}
    }
}
