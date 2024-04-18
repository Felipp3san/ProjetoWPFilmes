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

        // Passa para a página seguinte do genero selecionado e atualiza a lista na View.
        public ICommand NextPageCommand { get; set; }

        // Ao clicar em algum filme da lista, redireciona para a página de pesquisa
        // com os detalhes do respetivo filme.
        public ICommand SearchMovieCommand { get; set; }

        // Armazena a lista de generos que está sendo exibida na comboBox
        // de seleção de generos.
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

        // Armazena o genero selecionado para ser utilizado na busca dos filmes.
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

        // Armazena a lista de filmes filtrada por genero que será exibida na view.
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

        // Armazena o número atual da página para ser utilizado pelo 
        // comando de troca de página e pela filtragem de filmes.
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

        /// <summary>
        /// Incrementa o número da página.
        /// </summary>
        private void OnPageNumberChanged()
        {
            PageNumber++;
        }

        /// <summary>
        /// Inicializa a propriedade 'Genres' com a lista de 
        /// generos resgatada da DB.
        /// </summary>
        /// <returns></returns>
        private async Task InitializeGenreList()
        {
            Genres = await genreContext.GetGenres();
        }

        /// <summary>
        /// Inicializa a propriedade 'MovieListByGenre' com a 
        /// lista de filmes mais populares para preenchimento inicial 
        /// da secção de busca por genero.
        /// </summary>
        private async Task InitializeMovies()
        {
            MovieListByGenre = await movieContext.GetPopularMovies();
        }

        /// <summary>
        /// Inicializa a propriedade 'MovieListByGenre' com a lista
        /// de filmes do genero selecionado e página escolhida.
        /// </summary>
        /// <param name="genreId">ID do genero do filme</param>
        /// <param name="pageNumber">Número da página (20 filmes por página)</param>
        private async Task GetMoviesByGenre(int genreId, int pageNumber)
        {
            MovieListByGenre = await movieContext.GetMoviesByGenre(genreId, pageNumber);
        }
    }
}
