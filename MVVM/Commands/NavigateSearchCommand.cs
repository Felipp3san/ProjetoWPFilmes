using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    public class NavigateSearchCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateSearchCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new SearchViewModel(); 
        }
    }
}
