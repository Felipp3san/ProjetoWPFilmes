using System.Windows;

namespace MovieApp.MVVM.Commands
{
    class CloseCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
