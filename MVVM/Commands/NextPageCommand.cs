using MovieApp.MVVM.ViewModel;

namespace MovieApp.MVVM.Commands
{
    internal class NextPageCommand : CommandBase
    {
        public event Action? PageNumberChanged;
        private GenreViewModel _genreViewModel;

        public NextPageCommand(GenreViewModel genreViewModel)
        {
            _genreViewModel = genreViewModel;
            _genreViewModel.GenreSelectionChanged += OnGenreSelection;
        }

        private void OnGenreSelection()
        {
            OnExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _genreViewModel.SelectedGenre != 0;
        }

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
