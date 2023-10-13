// Import necessary namespaces.
using NotetakingApp_Babette.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// Define the namespace for the ViewModel.
namespace NotetakingApp_Babette.ViewModels
{
    // The NoteViewModel class implements INotifyPropertyChanged to notify the View of property changes.
    public class NoteViewModel : INotifyPropertyChanged
    {
        // ObservableCollection for notes ensures that any changes made (add, delete, update) are automatically reflected in the View.
        public ObservableCollection<Note> NotesCollection { get; private set; }

        // Private field for the currently selected or viewed note.
        private Note _currentNote;

        // Event to notify any subscribers (typically the UI) about changes to a property.
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor initializes the NotesCollection with some test data.
        public NoteViewModel()
        {
            NotesCollection = new ObservableCollection<Note>
            {
                new Note { Id = 1, Title = "test 1", Content = "test 1 content" },
                new Note { Id = 2, Title = "test 2", Content = "test 2 content" },
                new Note { Id = 3, Title = "test 3", Content = "test 3 content" },
            };
        }

        // Property for the current note. When set, it raises the OnPropertyChanged event to notify the UI of changes.
        public Note CurrentNote
        {
            get => _currentNote;
            set
            {
                _currentNote = value;
                OnPropertyChanged();
            }
        }

        // Method to add a new note or update an existing one in the NotesCollection.
        public void AddOrUpdateNote(Note note)
        {
            var existingNote = NotesCollection.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote != null)
            {
                var index = NotesCollection.IndexOf(existingNote);
                NotesCollection[index] = note;
            }
            else
            {
                NotesCollection.Add(note);
            }
        }

        // Method to delete the current note from the NotesCollection.
        public void DeleteCurrentNote()
        {
            if (_currentNote != null)
            {
                NotesCollection.Remove(_currentNote);
            }
        }

        // Method to get the next available ID for a new note. This ensures each note has a unique ID.
        public int GetNextNoteId()
        {
            return (NotesCollection.Max(note => note.Id)) + 1;
        }

        // Raises the PropertyChanged event. Utilizes the CallerMemberName attribute to automatically obtain the property name of the caller.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
