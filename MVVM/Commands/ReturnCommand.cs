using MovieApp.MVVM.Stores;
using MovieApp.MVVM.ViewModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MovieApp.MVVM.Commands
{
    internal class ReturnCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;

        public ReturnCommand(NavigationStore navigationStore) 
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public void OnCurrentViewModelChanged()
        {
            OnExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return ((ViewModelBase)_navigationStore.CurrentViewModel).PreviousViewModel != null && _navigationStore.CurrentViewModel.GetType() != typeof(HomeViewModel);
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = ((ViewModelBase)_navigationStore.CurrentViewModel).PreviousViewModel;
        }
    }
}
