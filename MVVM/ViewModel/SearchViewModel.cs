using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;

namespace MovieApp.MVVM.ViewModel
{
    internal class SearchViewModel : ViewModelBase
    {
		private MovieContext movieContext;
		private CastContext castContext;
		private MovieProviderContext movieProviderContext;

		// Armazena o filme que está sendo exibido na página de pesquisa (após a pesquisa ser finalizada).
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

		// Construtor vazio, utilizado quando secção de pesquisa é acessada sem que a caixa de pesquisa esteja preenchida.
		public SearchViewModel()
		{
			movieContext = new MovieContext();
			castContext = new CastContext();
			movieProviderContext = new MovieProviderContext();

            InitializeMovie();
        }

		// Construtor com parametro, utilizado quando secção de pesquisa é acessada a partir da caixa de pesquisa, preenchida com o nome de um filme.
        public SearchViewModel(string movieName)
		{
			movieContext = new MovieContext();
			castContext = new CastContext();
			movieProviderContext = new MovieProviderContext();

			InitializeMovie(movieName).GetAwaiter();
		}

		/// <summary>
		/// Busca o filme mais popular da DB e repassa para a propriedade SelectedMovie 
		/// para que a secção de pesquisa não seja inicializada vazia.
		/// Método utilizado quando a secção de pesquisa é acessada a partir do menu lateral.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Busca o filme introduzido na caixa de pesquisa e repassa para a propriedade SelectedMovie 
		/// para que seja exibido na página de pesquisa.
		/// Método utilizado quando a secção de pesquisa é acessada a partir da caixa de pesquisa.
		/// </summary>
		/// <returns></returns>
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
