using MovieApp.Core;
using MovieApp.MVVM.View;
using MovieApp.MVVM.View.TopControls;
using System.Windows;

namespace MovieApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        #region Button Commands Properties
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand PopularViewCommand { get; set; }
        public RelayCommand GenreViewCommand { get; set; }
		public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand CloseButtonCommand { get; set; }

        #endregion

        #region ViewModels

        public HomeViewModel HomeViewModel { get; set; }
        public PopularViewModel PopularViewModel { get; set; }
        public GenreViewModel GenreViewModel { get; set; }
		public SearchViewModel SearchViewModel { get; set; }

        #endregion

        #region View Setters

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

		private object currentMenu;
		public object CurrentMenu 
		{
			get { return currentMenu; }
			set 
			{ 
				currentMenu = value;
				OnPropertyChanged();
			}
		}

        #endregion

		public MainViewModel()
		{
			Image.InitializeImgDetails();			

			HomeViewModel = new HomeViewModel();
			PopularViewModel = new PopularViewModel();
			GenreViewModel = new GenreViewModel();
			SearchViewModel = new SearchViewModel();

			CurrentView = HomeViewModel;

			HomeViewCommand = new RelayCommand(o =>
			{
				CurrentView = HomeViewModel;
			});

			PopularViewCommand = new RelayCommand(o =>
			{
				CurrentView = PopularViewModel;
			});

			GenreViewCommand = new RelayCommand(o =>
			{
				CurrentView = GenreViewModel;
			});
			
			SearchViewCommand = new RelayCommand(o =>
			{
				CurrentView = SearchViewModel;
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
