using MovieApp.Core;
using System.Windows;

namespace MovieApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
		public RelayCommand HomeViewCommand { get; set; }
		public RelayCommand PopularViewCommand { get; set; }
		public RelayCommand CloseButtonCommand { get; set; }

        public HomeViewModel homeViewModel { get; set; }
        public PopularViewModel popularViewModel { get; set; }

        private object currentView;

		public object CurrentView 
		{
			get { return currentView; }
			set
			{ 
				currentView = value;
				OnPropertyChanged();
            }
		}

		public MainViewModel()
		{
			homeViewModel = new HomeViewModel();
			popularViewModel = new PopularViewModel();

			CurrentView = homeViewModel;

			HomeViewCommand = new RelayCommand(o =>
			{
				CurrentView = homeViewModel;
			});

			PopularViewCommand = new RelayCommand(o =>
			{
				CurrentView = popularViewModel;
			});

			CloseButtonCommand = new RelayCommand(o =>
			{
				CloseApplication();	
			});
		}

		private void CloseApplication()
		{
			Application.Current.Shutdown();
		}
    }
}
