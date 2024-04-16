namespace MovieApp.MVVM.Commands
{
    public class NextPageCommand : CommandBase
    {
        public event Action? PageNumberChanged;

        public override void Execute(object? parameter)
        {
            OnPageNumberChange();
        }

        public void OnPageNumberChange()
        {
            PageNumberChanged?.Invoke();
        }
    }
}
