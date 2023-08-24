using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NoteApp.DTOs;
using SEDC.NoteApp.Models;

namespace SEDC.NoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<NoteDto>> Get()
        {
            try
            {
                var notesDb = StaticDb.Notes;
                var notes = notesDb.Select(x => new NoteDto
                {
                    Priority = x.Priority,
                    Text = x.Text,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags.Select(t => t.Name).ToList(),

                }).ToList();
                return Ok(notes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest("The id cannot be negative!");
                }
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if(noteDb == null)
                {
                    return NotFound($"Note with id {id} was not found!");
                }

                var noteDto = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(t => t.Name).ToList()
                };
                return Ok(noteDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpGet("findById")]
        public ActionResult<NoteDto> FindById(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id cannot be negative!");
                }
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} was not found!");
                }
                var noteDto = new NoteDto
                {
                    Priority = noteDb.Priority,
                    Text = noteDb.Text,
                    User = $"{noteDb.User.FirstName} {noteDb.User.LastName}",
                    Tags = noteDb.Tags.Select(t => t.Name).ToList()
                };
                return Ok(noteDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpGet("user/{userId}")]
        public ActionResult<List<NoteDto>> FindNoteByUser(int userId)
        {
            try
            {
                var userNotes = StaticDb.Notes.Where(x =>x.UserId == userId).ToList();
                var userNotesDto = userNotes.Select(x => new NoteDto
                {
                    Priority = x.Priority,
                    Text = x.Text,
                    User = $"{x.User.FirstName} {x.User.LastName}",
                    Tags = x.Tags.Select(t => t.Name).ToList(),
                });
                return Ok(userNotesDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNoteById(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id cannot be negative!");
                }
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {id} was not found!");
                }
                StaticDb.Notes.Remove(noteDb);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
        [HttpPut]
        public IActionResult UpdateNote([FromBody] UpdateNoteDto updateNoteDto)
        {
            try
            {
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == updateNoteDto.Id);
                if (noteDb == null)
                {
                    return NotFound($"Note with id {updateNoteDto.Id} not found!");
                }
                if (string.IsNullOrEmpty(updateNoteDto.Text))
                {
                    return BadRequest("Text is a required field");
                }
                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == updateNoteDto.UserId);
                if (userDb == null)
                {
                    return NotFound($"User with id {updateNoteDto.UserId} was not found!");
                }
                List<Tag> tags = new List<Tag>();
                foreach (int tagsId in updateNoteDto.TagsId)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagsId);
                    if (tagDb == null)
                    {
                        return NotFound($"Tag with id {tagsId} was not found");
                    }
                    tags.Add(tagDb);
                }
                noteDb.Text = updateNoteDto.Text;
                noteDb.Priority = updateNoteDto.Priority;
                noteDb.UserId = userDb.Id;
                noteDb.User = userDb;
                noteDb.Tags = tags;

                return StatusCode(StatusCodes.Status204NoContent, "Note updated!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }            
        }
        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody]AddNoteDto addNoteDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(addNoteDto.Text))
                {
                    return BadRequest("Text is a required field");
                }
                User userDb = StaticDb.Users.FirstOrDefault(x => x.Id == addNoteDto.UserId);
                if (userDb == null)
                {
                    return NotFound($"User with Id {addNoteDto.UserId} was not found!");
                }
                List<Tag> tags = new List<Tag>();
                foreach(int tagId in addNoteDto.TagsId)
                {
                    Tag tagDb = StaticDb.Tags.FirstOrDefault(x => x.Id == tagId);
                    if(tagDb == null)
                    {
                        return NotFound($"Tag with Id {tagId} was not found!");
                    }
                    tags.Add(tagDb);
                }
                Note newNote = new Note
                {
                    Id = ++StaticDb.NoteId,
                    Text = addNoteDto.Text,
                    Priority = addNoteDto.Priority,
                    User = userDb,
                    UserId = addNoteDto.UserId,
                    Tags = tags
                };
                StaticDb.Notes.Add(newNote);
                return StatusCode(StatusCodes.Status201Created, "Note Created");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }
    }
}
