using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieApp.MVVM.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifica mudanças dos valores de propriedades as Views.
        /// </summary>
        /// <param name="property"></param>
        protected void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
