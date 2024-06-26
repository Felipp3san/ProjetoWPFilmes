﻿using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Stores;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class MainViewModel : ViewModelBase 
    {
		private readonly NavigationStore _navigationStore;

		// Atribui a View atual ao ContentControl, inicializado com 'Home' ao iniciar a aplicação.
		public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

		// Propriedades responsáveis por controlar o menu lateral e o botão que encerra a aplicação. 
		public ICommand NavigateHomeCommand { get; }
		public ICommand NavigatePopularCommand { get; }
		public ICommand NavigateGenreCommand { get; }
		public ICommand NavigateSearchCommand { get; }
		public ICommand ReturnCommand{ get; }
		public ICommand CloseButtonCommand { get; }

		// Propriedades responsáveis por gerenciar marcação de menu ao utilizar o botao return

		private bool isHomeChecked = true;

		public bool IsHomeChecked 
		{
			get { return isHomeChecked; }
			set 
			{
				isHomeChecked = value;
				OnPropertyChanged();
			}
		}

		private bool isGenreChecked;

        public bool IsGenreChecked
        {
            get { return isGenreChecked; }
            set
            {
                isGenreChecked = value;
                OnPropertyChanged();
            }
        }

        // Utilizada para armazenar o nome do filme a ser pesquisado.
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
			NavigateGenreCommand = new NavigateGenreCommand(navigationStore);
            NavigateSearchCommand = new SearchMovieCommand(navigationStore, () => MovieName);
			ReturnCommand = new ReturnCommand(navigationStore);
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

			// Modifica a seleção do radioButton
			if (_navigationStore.CurrentViewModel.GetType() == typeof(HomeViewModel))
				IsHomeChecked = true;
			else
				IsHomeChecked = false;

			if(_navigationStore.CurrentViewModel.GetType() == typeof(GenreViewModel))
				IsGenreChecked = true;
            else
                IsGenreChecked = false;

            // Envia uma notificação para view modificar a apresentação do ContentControl
            OnPropertyChanged(nameof(CurrentViewModel));
		}
    }
} 