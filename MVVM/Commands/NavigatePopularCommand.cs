using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    public class NavigatePopularCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigatePopularCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new PopularViewModel();
        }
    }
}
