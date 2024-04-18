using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Data;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Stores;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class HomeViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private MovieContext movieContext;

        // Comando responsável por chamar a página de pesquisa a partir do filme que foi clicado.
        public ICommand SearchMovieCommand { get; set; }

        // Armazena a lista de filmes (em exibição nos cinemas), que está sendo exibida na HomeView. 
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

        // Armazena a lista de filmes (adicionados recentemente a DB), que será exibida na HomeView. 
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

        /// <summary>
        /// Faz a busca dos filmes atualmente em exibição nos cinemas
        /// e repassa para a propriedade responsável por armazena-los para serem 
        /// exibidos na View.
        /// </summary>
        /// <returns></returns>
        private async Task InitializeTheaterMovieList()
        {
            TheaterMovieList = await movieContext.GetTheaterMovies();
        }

        /// <summary>
        /// Faz a busca dos filmes adicionado recentemente a DB do TheMovieDB 
        /// e repassa para a propriedade responsável por armazena-los para serem 
        /// exibidos na View.
        /// </summary>
        /// <returns></returns>
        private async Task InitializeLatestMovies()
        {
            LatestMovieList = await movieContext.GetLatestMovies(); 
        }
    }
}
