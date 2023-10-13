// Import necessary namespaces for the functionality of this page.
using NotetakingApp_Babette.Model;
using NotetakingApp_Babette.ViewModels;

// Define the namespace for organizing related pages within the Notetaking App.
namespace NotetakingApp_Babette.Pages
{
    // Define the MakeNotePage class, which is responsible for creating or editing a note.
    // This class inherits from the ContentPage class provided by the MAUI framework, which represents a single screen.
    public partial class MakeNotePage : ContentPage
    {
        // Declare a private read-only instance of the NoteViewModel.
        // This ViewModel provides data and operations related to notes.
        private readonly NoteViewModel viewModel;

        // Constructor for MakeNotePage which accepts the NoteViewModel instance from the calling page.
        public MakeNotePage(NoteViewModel viewModel)
        {
            // Initialize the UI components for this page.
            InitializeComponent();

            // Assign the passed viewModel to the private field to be used in this page.
            this.viewModel = viewModel;

            // Set the BindingContext for data binding.
            // It allows UI elements to be bound to properties in the ViewModel.
            BindingContext = this.viewModel;

            // Populate the title and content fields based on the currently selected note (if it exists).
            // The '?' is a null-conditional operator, which prevents null reference exceptions.
            titleEntry.Text = viewModel.CurrentNote?.Title;
            contentEditor.Text = viewModel.CurrentNote?.Content;
        }

        // Event handler for when the back button is clicked.
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Check if there's a selected note in the ViewModel.
            // This should usually be the case, but it's good to have a safety check.
            if (viewModel.CurrentNote == null)
            {
                return; // Exit the method if there's no note.
            }

            // Update the title and content of the current note based on user input.
            viewModel.CurrentNote.Title = titleEntry.Text;
            viewModel.CurrentNote.Content = contentEditor.Text;

            // Use the ViewModel to add or update the note in the collection.
            viewModel.AddOrUpdateNote(viewModel.CurrentNote);

            // Navigate back to the previous page (MainPage in this case).
            await Navigation.PopAsync();
        }

        // Event handler for when the delete button is clicked.
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // Check if there's a selected note to delete.
            if (viewModel.CurrentNote != null)
            {
                // Use the ViewModel to delete the current note.
                viewModel.DeleteCurrentNote();

                // Navigate back to the previous page after the deletion.
                await Navigation.PopAsync();
            }
        }
    }
}
