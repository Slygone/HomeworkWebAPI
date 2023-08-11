using NotesApp.Models.Enum;

namespace NotesApp.Models
{
    public class Note : BaseEntity
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
