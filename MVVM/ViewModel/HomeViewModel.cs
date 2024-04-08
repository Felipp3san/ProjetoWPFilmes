using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;

namespace MovieApp.MVVM.ViewModel
{
    internal class HomeViewModel : ViewModelBase
    {
        private MovieContext movieContext;

        private List<Movie> theaterMovieList;
        public List<Movie> TheaterMovieList 
        {
            get { return theaterMovieList; }
            set 
            {
                theaterMovieList = value;
                OnPropertyChanged();
            }
        }

        private List<Movie> latestMovieList;
        public List<Movie> LatestMovieList 
        {
            get { return latestMovieList; }
            set 
            {
                latestMovieList = value;
                OnPropertyChanged();
            }
        }
        
        public HomeViewModel()
        {
            movieContext = new MovieContext();

            InitializeTheaterMovieList();
            InitializeLatestMovies();
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
