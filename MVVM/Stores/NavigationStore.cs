using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;


        // Propriedade que armazena a view selecionada
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

        // Método do evento CurrentViewModelChanged que notifica o MainViewModel quando há modificação no CurrentView (Opção de menu selecionada)
        public void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
