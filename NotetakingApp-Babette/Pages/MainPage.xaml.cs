using NotetakingApp_Babette.Model;
using NotetakingApp_Babette.ViewModels;

namespace NotetakingApp_Babette.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly NoteViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new NoteViewModel();
            this.BindingContext = viewModel;
            notesView.ItemsSource = viewModel.NotesCollection;
        }

        private async void OnNoteSelected(object sender, EventArgs e)
        {
            if (sender is Label label && label.BindingContext is Note tappedNote)
            {
                viewModel.CurrentNote = tappedNote;
                var makeNotePage = new MakeNotePage(viewModel);  // Pass the ViewModel instance to MakeNotePage
                await Navigation.PushAsync(makeNotePage);
            }
        }

        private async void OnAddNewNoteClickedAsync(object sender, EventArgs e)
        {
            var newNote = new Note { Id = viewModel.GetNextNoteId() };
            viewModel.CurrentNote = newNote; // Setting the current note in ViewModel
            var makeNotePage = new MakeNotePage(viewModel);  // Pass the ViewModel instance to MakeNotePage
            await Navigation.PushAsync(makeNotePage);
        }

        // No need for AddNote, UpdateNote, DeleteNote here. 
        // All of those operations are now handled in the ViewModel.
    }
}
