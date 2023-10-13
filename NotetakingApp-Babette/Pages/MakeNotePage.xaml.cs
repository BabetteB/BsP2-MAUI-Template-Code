using NotetakingApp_Babette.Model;
using NotetakingApp_Babette.ViewModels;

namespace NotetakingApp_Babette.Pages
{
    public partial class MakeNotePage : ContentPage
    {
        private readonly NoteViewModel viewModel;

        public MakeNotePage(NoteViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;

            // Set fields based on CurrentNote
            titleEntry.Text = viewModel.CurrentNote?.Title;
            contentEditor.Text = viewModel.CurrentNote?.Content;
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (viewModel.CurrentNote == null)
            {
                // Just to ensure we have a note to work with. 
                // Ideally, this should never be the case because of the MainPage setup.
                return;
            }

            viewModel.CurrentNote.Title = titleEntry.Text;
            viewModel.CurrentNote.Content = contentEditor.Text;

            viewModel.AddOrUpdateNote(viewModel.CurrentNote);

            await Navigation.PopAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (viewModel.CurrentNote != null)
            {
                viewModel.DeleteCurrentNote();
                await Navigation.PopAsync();
            }
        }
    }
}
