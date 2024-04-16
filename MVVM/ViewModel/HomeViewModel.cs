using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Stores;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class HomeViewModel : ViewModelBase
    {
        private MovieContext movieContext;
        private readonly NavigationStore _navigationStore;
        public ICommand SearchMovieCommand { get; set; }


        private List<Movie>? theaterMovieList;
        public List<Movie>? TheaterMovieList 
        {
            get { return theaterMovieList; }
            set 
            {
                theaterMovieList = value;
                OnPropertyChanged();
            }
        }

        private List<Movie>? latestMovieList;
        public List<Movie>? LatestMovieList 
        {
            get { return latestMovieList; }
            set 
            {
                latestMovieList = value;
                OnPropertyChanged();
            }
        }
        
        public HomeViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            movieContext = new MovieContext();

            SearchMovieCommand = new SearchMovieCommand(_navigationStore);

            InitializeTheaterMovieList().GetAwaiter();
            InitializeLatestMovies().GetAwaiter();
        }

        private async Task InitializeTheaterMovieList()
        {
            TheaterMovieList = await movieContext.GetTheaterMovies();
        }

        private async Task InitializeLatestMovies()
        {
            LatestMovieList = await movieContext.GetLatestMovies(); 
        }
    }
}
