using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;
using System.Windows;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
        
    }

}
