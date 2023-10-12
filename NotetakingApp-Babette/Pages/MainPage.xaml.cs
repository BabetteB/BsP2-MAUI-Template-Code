using NotetakingApp_Babette.Model;
using System.Collections.ObjectModel;

namespace NotetakingApp_Babette.Pages;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Note> NotesCollection { get; private set; }
    public MainPage()
	{
        InitializeComponent();
        this.BindingContext = this;

        NotesCollection = new ObservableCollection<Note>
        {
            new Note { Id = 1, Title = "test 1", Content = "test 1 content" },
            new Note { Id = 2, Title = "test 2", Content = "test 2 content" },
            new Note { Id = 3, Title = "test 3", Content = "test 3 content" },
        };

        notesView.ItemsSource = NotesCollection;

        // Subscribing to the "AddNote" message
        MessagingCenter.Subscribe<MakeNotePage, Note>(this, "AddNote", (sender, arg) =>
        {
            if (arg.Id == 1)
            {
                NotesCollection.Add(arg); // arg is the new Note
            }
        });

        // Subscribing to the "UpdateNote" message
        MessagingCenter.Subscribe<MakeNotePage, Note>(this, "UpdateNote", (sender, arg) =>
        {
            var existingNote = NotesCollection.FirstOrDefault(n => n.Title == arg.Title);
            if (existingNote != null)
            {
                existingNote.Content = arg.Content;
            }
        });

    }

    private async void OnNoteSelected(object sender, EventArgs e)
    {
        if (sender is Label label && label.BindingContext is Note tappedNote)
        {
            var navigationParameter = new Dictionary<string, object> { { "SelectedNote", tappedNote } };
            MakeNotePage.NoteDeleted += OnNoteDeleted;
            await Shell.Current.GoToAsync($"//{nameof(MakeNotePage)}?IsNewNote=false", navigationParameter);
        }
    }

    private void OnNoteDeleted(Note deletedNote)
    {
        var notes = notesView.ItemsSource as ObservableCollection<Note>;
        var noteToRemove = notes.FirstOrDefault(n => n.Id == deletedNote.Id);
        if (noteToRemove != null)
        {
            notes.Remove(noteToRemove);
        }

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MakeNotePage.NoteDeleted -= OnNoteDeleted;
    }



    private async void OnAddNewNoteClickedAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//MakeNotePage?IsNewNote=true");
    }


}

