using Microsoft.EntityFrameworkCore.Migrations;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Note;
using SEDC.NotesApp.Mappers.Notes;
using SEDC.NotesApp.Services.Interfaces;

namespace SEDC.NotesApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        public readonly IRepository<Note> _noteRepository;
        public readonly IRepository<User> _userRepository;

        public NoteService(IRepository<Note> noteRepository, IRepository<User> userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new Exception("Text cannot be empty");
            }
            User userDb = _userRepository.GetById(addNoteDto.UserId);
            if(userDb == null)
            {
                throw new Exception("Not found");
            }
            Note note = addNoteDto.ToNote();
            _noteRepository.Add(note);
        }

        public List<NoteDto> GetAll()
        {
            //1. get all notes from db
            List<Note> notesDb = _noteRepository.GetAll();
            //2. map to dtos and return to caller
            return notesDb.Select(x => x.ToNoteDto()).ToList(); 
        }

        public NoteDto GetById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            
            if(noteDb == null)
            {
                throw new Exception("Not found!");
            }
            return noteDb.ToNoteDto();
        }
    }
}
