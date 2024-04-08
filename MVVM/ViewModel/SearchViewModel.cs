using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;

namespace MovieApp.MVVM.ViewModel
{
    internal class SearchViewModel : ViewModelBase
    {
		private readonly string _movieName;

		private MovieContext movieContext;
		private CastContext castContext;

		private Movie selectedMovie;

		public Movie SelectedMovie 
		{
			get { return selectedMovie; }
			set
			{
				selectedMovie = value;
				OnPropertyChanged();
			}
		}

		public SearchViewModel(string movieName)
		{
			movieContext = new MovieContext();
			castContext = new CastContext();

			_movieName = movieName;
			InitializeMovie(_movieName);
		}

		private async Task InitializeMovie(string? movieName)
		{
			int movieId;

			if(string.IsNullOrEmpty(movieName))
			{
				movieId = 693134;
			}
            else
            {
               movieId = await GetMovieId(movieName); 
            }

            Movie movie = await movieContext.GetMovieById(movieId);
			movie.Cast = await castContext.GetCastByMovieId(movieId);

			SelectedMovie = movie;
		}


		private async Task<int> GetMovieId(string movieName)
		{
			int movieId = await movieContext.GetMovieId(movieName);

			return movieId;
		}
    }
}
