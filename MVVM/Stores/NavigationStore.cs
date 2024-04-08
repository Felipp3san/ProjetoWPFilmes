using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel 
        {
            get { return currentViewModel; }
            set 
            {
                currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
