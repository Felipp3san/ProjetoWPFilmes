using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;


        // Armazena a View selecionada que será exibida no ContentControl do MainView. 
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

        // Notifica o MainViewModel quando há modificação no CurrentView (Opção de menu selecionada).
        public void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
