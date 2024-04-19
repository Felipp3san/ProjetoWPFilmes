using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Stores;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class SearchViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly MovieContext _movieContext;

        public ICommand MovieDetailsCommand { get; set; }

        private List<Movie> searchedMovies;
		public List<Movie> SearchedMovies 
		{
			get { return searchedMovies; }
			set 
			{ 
				searchedMovies = value;
                OnPropertyChanged();
			}
		}

		// Controla a visibilidade do textBlock "Filme não encontrado."
		private string movieNotFoundVisibility;

		public string MovieNotFoundVisibility 
		{
			get { return movieNotFoundVisibility; }
			set 
			{
				movieNotFoundVisibility = value;
				OnPropertyChanged();
			}
		}

		public SearchViewModel(NavigationStore navigationStore, ViewModelBase previousViewModel ,string movieName)
		{
			_navigationStore = navigationStore;	
			_movieContext = new MovieContext();

			PreviousViewModel = previousViewModel;
			MovieDetailsCommand = new NavigateDetailsCommand(navigationStore);

			GetMoviesByName(movieName).GetAwaiter();
		}

		private async Task GetMoviesByName(string movieName)
		{
            SearchedMovies = await _movieContext.GetMoviesByName(movieName);

            if (SearchedMovies.Count == 0)
				MovieNotFoundVisibility = "Visible";                
            else
				movieNotFoundVisibility = "Hidden";
        }
	}
}
