using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    class NavigateHomeCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateHomeCommand(NavigationStore navigationStore) 
        { 
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new HomeViewModel(); 
        }
    }
}
