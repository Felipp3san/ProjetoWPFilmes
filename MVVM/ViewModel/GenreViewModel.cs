using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Stores;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class GenreViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        GenreContext genreContext;
        MovieContext movieContext;

        public ICommand NextPageCommand { get; set; }
        public ICommand SearchMovieCommand { get; set; }

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
                GetMoviesByGenre(selectedGenre, PageNumber).GetAwaiter();
            }
        }

        public GenreViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            genreContext = new GenreContext();
            movieContext = new MovieContext();
            NextPageCommand = new NextPageCommand(); 

            NextPageCommand.CanExecute(SelectedGenre != 0);
            ((NextPageCommand) NextPageCommand).PageNumberChanged += OnPageNumberChanged;

            SearchMovieCommand = new SearchMovieCommand(_navigationStore);

            InitializeGenreList().GetAwaiter();
            InitializeMovies().GetAwaiter();
        }

        private void OnPageNumberChanged()
        {
            PageNumber++;
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
    }
}
