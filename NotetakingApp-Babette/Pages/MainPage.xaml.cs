// Import the necessary namespaces that provide the Note model and the NoteViewModel.
using NotetakingApp_Babette.Model;
using NotetakingApp_Babette.ViewModels;

// Define the namespace for organizing related pages within the Notetaking App.
namespace NotetakingApp_Babette.Pages
{
    // Define the MainPage class which is the primary view for displaying and navigating between notes.
    // This class inherits from the ContentPage class provided by the MAUI framework, which represents a single screen.
    public partial class MainPage : ContentPage
    {
        // Declare a private read-only instance of the NoteViewModel.
        // This ViewModel will provide the data and operations related to notes for this page.
        private readonly NoteViewModel viewModel;

        // Default constructor for the MainPage.
        public MainPage()
        {
            // Initialize the UI components for this page.
            InitializeComponent();

            // Create a new instance of NoteViewModel.
            viewModel = new NoteViewModel();

            // Set the BindingContext to the viewModel for data binding.
            // This allows UI elements in MainPage to bind to properties in the ViewModel.
            this.BindingContext = viewModel;

            // Bind the notesView's ItemsSource to the NotesCollection from the ViewModel.
            // This will display all the notes in the collection.
            notesView.ItemsSource = viewModel.NotesCollection;
        }

        // Event handler for when a note is selected (tapped) from the list.
        private async void OnNoteSelected(object sender, EventArgs e)
        {
            // Check if the sender of this event is a Label and that its context is a Note.
            if (sender is Label label && label.BindingContext is Note tappedNote)
            {
                // Set the CurrentNote in the ViewModel to the tappedNote.
                viewModel.CurrentNote = tappedNote;

                // Create a new MakeNotePage to edit/view the selected note and pass the ViewModel instance to it.
                var makeNotePage = new MakeNotePage(viewModel);

                // Navigate to the MakeNotePage.
                await Navigation.PushAsync(makeNotePage);
            }
        }

        // Event handler for when the "Add New Note" button is clicked.
        private async void OnAddNewNoteClickedAsync(object sender, EventArgs e)
        {
            // Create a new note with a new Id obtained from the ViewModel's method.
            var newNote = new Note { Id = viewModel.GetNextNoteId() };

            // Set the new note as the current note in the ViewModel.
            viewModel.CurrentNote = newNote;

            // Create a new MakeNotePage to create a new note and pass the ViewModel instance to it.
            var makeNotePage = new MakeNotePage(viewModel);

            // Navigate to the MakeNotePage.
            await Navigation.PushAsync(makeNotePage);
        }

        // Operations like AddNote, UpdateNote, and DeleteNote have been removed from MainPage.
        // These are now handled directly in the ViewModel for better separation of concerns.
    }
}
