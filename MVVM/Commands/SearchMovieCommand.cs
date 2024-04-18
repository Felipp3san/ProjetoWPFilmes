using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    public class SearchMovieCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public SearchMovieCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            int movieId = (int)parameter;
            _navigationStore.CurrentViewModel = new SearchViewModel(movieId);
        }
    }
}
