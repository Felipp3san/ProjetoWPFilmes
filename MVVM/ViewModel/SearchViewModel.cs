using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class SearchViewModel : ViewModelBase
    {
		private MovieContext movieContext;
		private CastContext castContext;
		private MovieProviderContext movieProviderContext;

		public ICommand ProviderCommand { get; set; }

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

        public SearchViewModel(int movieId)
		{
			movieContext = new MovieContext();
			castContext = new CastContext();
			movieProviderContext = new MovieProviderContext();

            ProviderCommand = new GetProviderPageCommand();

            SearchMovieById(movieId).GetAwaiter();
		}

		/// <summary>
		/// Busca o filme seleciona na lista de pesquisa e repassa para a propriedade SelectedMovie 
		/// para que seja exibido na página de pesquisa.
		/// Método utilizado quando a secção de pesquisa é acessada a partir da caixa de pesquisa.
		/// </summary>
		/// <returns></returns>
		private async Task SearchMovieById(int movieId)
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
