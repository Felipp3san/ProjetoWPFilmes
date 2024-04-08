using MovieApp.MVVM.Commands;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Stores;
using System.Windows.Input;

namespace MovieApp.MVVM.ViewModel
{
    internal class MainViewModel : ViewModelBase 
    {
		private readonly NavigationStore _navigationStore;

		public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

		public ICommand NavigateHomeCommand { get; }
		public ICommand NavigatePopularCommand { get; }
		public ICommand NavigateGenreCommand { get; }
		public ICommand NavigateSearchCommand { get; }
		public ICommand CloseButtonCommand { get; }

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
			_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

			NavigateHomeCommand = new NavigateHomeCommand(navigationStore);
			NavigatePopularCommand = new NavigatePopularCommand(navigationStore);
			NavigateGenreCommand = new NavigateGenreCommand(navigationStore);
			NavigateSearchCommand = new NavigateSearchCommand(navigationStore, () => MovieName);
			CloseButtonCommand = new CloseCommand();

			Image.InitializeImgDetails();			
		}

        private void OnCurrentViewModelChanged()
        {
			MovieName = string.Empty;
            OnPropertyChanged(nameof(CurrentViewModel));
		}
    }
}
