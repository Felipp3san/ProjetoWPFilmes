using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Stores;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class MainViewModel : ViewModelBase 
    {
		private readonly NavigationStore _navigationStore;

		// Propriedade utilizada para atribuir a View atual ao ContentControl, inicializado com 'Home' ao iniciar aplicação.
		public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

		// Propriedade utilizada para alterar seleção do menu lateral caso a secção
		// de pesquisa seja acessada a partir da caixa de pesquisa ou ao clicar em um filme.
		private bool isChecked = false;
		public bool IsChecked 
		{
			get { return isChecked; }
			set 
			{ 
				isChecked = value;
				OnPropertyChanged();
			}
		}

		// Propriedades responsáveis por controlar o menu lateral e o botão que encerra a aplicação. 
		public ICommand NavigateHomeCommand { get; }
		public ICommand NavigatePopularCommand { get; }
		public ICommand NavigateGenreCommand { get; }
		public ICommand NavigateSearchCommand { get; }
		public ICommand NavigateSearchBarCommand { get; }
		public ICommand CloseButtonCommand { get; }


		// Propriedade utilizada para capturar o nome do filme a ser pesquisado.
		private string movieName = string.Empty;
		public string MovieName
		{
			get { return movieName; }
			set
			{
				movieName = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel(NavigationStore navigationStore)
		{
			_navigationStore = navigationStore;

			// Associa o metodo OnCurrentViewModel ao evento do NavigationStore que notifica alterações de View.
			_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

			// Inicializaão das propriedades dos botões de navegação
			NavigateHomeCommand = new NavigateHomeCommand(navigationStore);
			NavigatePopularCommand = new NavigatePopularCommand(navigationStore);
			NavigateGenreCommand = new NavigateGenreCommand(navigationStore);
			NavigateSearchCommand = new NavigateSearchCommand(navigationStore);
			NavigateSearchBarCommand = new NavigateSearchBarCommand(navigationStore, () => MovieName);
			CloseButtonCommand = new CloseCommand();

			// Inicializa a classe estática 'Image' com os dados necessários para se utilizar na aplicação durante sua execução.
			Image.InitializeImgDetails();			
		}

		/// <summary>
		/// Responsável por notificar a View sobre mudanças da propriedade CurrentView.
		/// A View altera o conteúdo do ContentControl após receber a notificação, 
		/// para que a view selecionada seja exibida corretamente.
		/// </summary>
        private void OnCurrentViewModelChanged()
        {
			// Limpa a caixa de pesquisa de filmes
			MovieName = string.Empty;

			// Sempre que um filme for pesquisado, a seleção do menu vai para a aba 'Pesquisar' 			
			if(CurrentViewModel.GetType() == typeof(SearchViewModel))
				IsChecked = true;

			// Envia uma notificação para view modificar a apresentação do ContentControl
            OnPropertyChanged(nameof(CurrentViewModel));
		}
    }
} 