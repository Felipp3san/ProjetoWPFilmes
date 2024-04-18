using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    public class NavigateDetailsCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateDetailsCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            int movieId = (int)parameter;

            _navigationStore.CurrentViewModel = new DetailsViewModel(movieId, _navigationStore.CurrentViewModel);
        }
    }
}
