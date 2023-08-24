using SEDC.NotesApp.Dtos.Note;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAll();
        NoteDto GetById(int id);

        void AddNote(AddNoteDto addNoteDto);

    }
}
