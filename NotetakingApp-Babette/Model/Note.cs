// Declare a namespace named "NotetakingApp_Babette.Model" to organize related classes, 
// in this case, the model classes of the Notetaking App.
namespace NotetakingApp_Babette.Model
{
    // Define a public class named "Note" to represent a single note in the application.
    public class Note
    {
        // Declare an integer property named "Id" to uniquely identify each note.
        // This is crucial for operations like updates or deletions where a specific note needs to be targeted.
        public int Id { get; set; }

        // Declare a string property named "Title" to represent the title or header of the note.
        // This can be used in listings or summaries to give users a quick glimpse of the note's content.
        public string Title { get; set; }

        // Declare a string property named "Content" to hold the main body or content of the note.
        // This is where the main details or information of the note will be stored.
        public string Content { get; set; }
    }
}
