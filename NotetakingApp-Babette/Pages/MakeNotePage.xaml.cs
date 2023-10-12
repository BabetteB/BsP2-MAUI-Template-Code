using NotetakingApp_Babette.Model;

namespace NotetakingApp_Babette.Pages;

[QueryProperty(nameof(NoteToDisplay), "SelectedNote")]
[QueryProperty(nameof(IsNewNoteParam), "IsNewNote")]
public partial class MakeNotePage : ContentPage
{
    public static event Action<Note> NoteDeleted;
    public bool IsNewNote { get; set; } = true;


    public MakeNotePage()
	{
		InitializeComponent();

    }

    public string IsNewNoteParam
    {
        set
        {
            IsNewNote = bool.Parse(value);
        }
    }


    public Note NoteToDisplay
    {
        set { DisplayCurrentNote(value); }
    }


	public static async void GoToMainPage()
	{
        await Shell.Current.GoToAsync("//MainPage");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

    private void DisplayCurrentNote(Note note)
    {
        if (note != null)
        {
            titleEntry.Text = note.Title;
            contentEditor.Text = note.Content;
        }
    }

    public void OnDeleteBtnClicked(object sender, EventArgs args)
    {
        if (!IsNewNote) // Only trigger the delete if it's not a new note
        {
            var noteToDelete = new Note
            {
                Title = titleEntry.Text.Trim(),
                Content = contentEditor.Text.Trim(),
            };
            NoteDeleted?.Invoke(noteToDelete);
        }
        GoToMainPage();
    }

    public void OnBackBtnClicked(object sender, EventArgs args)
    {
        var note = new Note
        {
            Title = titleEntry.Text.Trim(),
            Content = contentEditor.Text.Trim(),
        };

        if (IsNewNote) // If it's a new note, add it
        {
            if (!string.IsNullOrEmpty(note.Title) || !string.IsNullOrEmpty(note.Content))
                MessagingCenter.Send(this, "AddNote", note);
        }
        else // If it's an existing note, update it
        {
            // Logic to update the note in the list goes here.
            // For the sake of simplicity, the code currently replaces the old note with a new one. 
            // In a more complex application, you might prefer a database update or a more sophisticated mechanism.
            MessagingCenter.Send(this, "UpdateNote", note);
        }

        GoToMainPage();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MessagingCenter.Unsubscribe<MakeNotePage, Note>(this, "AddNote");
    }

}