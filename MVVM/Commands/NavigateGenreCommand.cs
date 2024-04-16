using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    public class NavigateGenreCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateGenreCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new GenreViewModel(_navigationStore);
        }
    }
}
