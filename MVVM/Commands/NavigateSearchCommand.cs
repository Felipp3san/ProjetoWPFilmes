using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    public class NavigateSearchCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<string> _getMovieName;

        public NavigateSearchCommand(NavigationStore navigationStore, Func<string> getMovieName)
        {
            _navigationStore = navigationStore;
            _getMovieName = getMovieName;
        }

        public override void Execute(object? parameter)
        {
            string movieName = _getMovieName();
            _navigationStore.CurrentViewModel = new SearchViewModel(movieName); 
        }
    }
}
