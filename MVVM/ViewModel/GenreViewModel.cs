using MovieApp.Core;
using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;

namespace MovieApp.MVVM.ViewModel
{
    internal class GenreViewModel : ObservableObject
    {
        #region Properties

        GenreContext genreContext;
        MovieContext movieContext;

        public RelayCommand NextPage { get; set; }
        public RelayCommandArguments<int> SelectedMovie{ get; set; }

        private GenreList genres;
        public GenreList Genres
        {
            get { return genres; }
            set
            {
                genres = value;
                OnPropertyChanged();
            }
        }

        private int selectedGenre;
        public int SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                selectedGenre = value;
                PageNumber = 1;
                OnPropertyChanged();
            }
        }

        private List<Movie> movieListByGenre;
        public List<Movie> MovieListByGenre
        {
            get { return movieListByGenre; }
            set
            { 
                movieListByGenre = value;
                OnPropertyChanged();
            }
        }

        private int pageNumber;

        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                pageNumber = value; 
                GetMoviesByGenre(selectedGenre, PageNumber);
            }
        }


        #endregion

        public GenreViewModel()
        {
            genreContext = new GenreContext();
            movieContext = new MovieContext();

            NextPage = new RelayCommand(execute => PageNumber++, canExecute => SelectedGenre != 0);
            SelectedMovie = new RelayCommandArguments<int>(execute => SelectedMovieId((int)execute)); 

            InitializeGenreList();
            InitializeMovies();
        }

        private async Task InitializeGenreList()
        {
            Genres = await genreContext.GetGenres();
        }

        private async Task InitializeMovies()
        {
            MovieListByGenre = await movieContext.GetPopularMovies();
        }

        private async Task GetMoviesByGenre(int genreId, int pageNumber)
        {
            MovieListByGenre = await movieContext.GetMoviesByGenre(genreId, pageNumber);
        }

        private void SelectedMovieId(int movieId)
        { 
        }
    }
}
