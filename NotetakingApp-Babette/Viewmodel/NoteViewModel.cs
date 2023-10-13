using NotetakingApp_Babette.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotetakingApp_Babette.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Note> NotesCollection { get; private set; }
        private Note _currentNote;

        public event PropertyChangedEventHandler PropertyChanged;

        public NoteViewModel()
        {
            NotesCollection = new ObservableCollection<Note>
            {
                new Note { Id = 1, Title = "test 1", Content = "test 1 content" },
                new Note { Id = 2, Title = "test 2", Content = "test 2 content" },
                new Note { Id = 3, Title = "test 3", Content = "test 3 content" },
            };
        }

        public Note CurrentNote
        {
            get => _currentNote;
            set
            {
                _currentNote = value;
                OnPropertyChanged();
            }
        }

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

        public void DeleteCurrentNote()
        {
            if (_currentNote != null)
            {
                NotesCollection.Remove(_currentNote);
            }
        }

        public int GetNextNoteId()
        {
            return (NotesCollection.Max(note => note.Id)) + 1;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
