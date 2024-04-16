using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    public class NavigateSearchBarCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<string> _getMovieName;

        public NavigateSearchBarCommand(NavigationStore navigationStore, Func<string> getMovieName)
        {
            _navigationStore = navigationStore;
            _getMovieName = getMovieName;
        }

        public NavigateSearchBarCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            string movieName = _getMovieName();

            if (string.IsNullOrEmpty(movieName))
            {
                _navigationStore.CurrentViewModel = new SearchViewModel();
            }
            else
            {
                _navigationStore.CurrentViewModel = new SearchViewModel(movieName);
            }
        }
    }
}
