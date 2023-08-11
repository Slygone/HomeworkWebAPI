using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //http://localhost:[port]/api/Notes
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Note>> Get()
        {
            try
            {
                return Ok(StaticDb.Notes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpGet("details/{id}")] //http://localhost:[port]/api/Notes/details/id
        //document which status codes can be returned by the action
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Note))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Note> GetNoteDetails(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The id cannot be negative!");
                }
                
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == id);
                if (id >= StaticDb.Notes.Count)
                {
                    return NotFound("No id found!");
                }
                return Ok(StaticDb.Notes[id]);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpGet("text/{text}/priority/{priority}")]
        //http:localhost:[port]api/notes/text/gym/priority/2
        //find all notes whose text contains gym and/or with priority medium
        //limitations:text is required, we need fixed parts from the route to make it more clear,
        //we can not switch places of the parameters

        public ActionResult<List<Note>> FilterNotes(string text, int priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return BadRequest("The text field can not be empty");
                }
                if(priority <= 0 || priority > 3)
                {
                    return BadRequest("Invalid priority value");
                }

                List<Note> notesDb = 
                    StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())
                    && (int) x.Priority == priority).ToList();
                return Ok(notesDb);  
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

        [HttpGet("filterNotes")]
        //http:localhost:[port]api/notes/filterNotes?text=gym&priority=1
        //http:localhost:[port]api/notes/filterNotes?priority=1&text=gym
        //http:localhost:[port]api/notes/filterNotes?priority=1
        //http:localhost:[port]api/notes/filterNotes?text=gym
        //http:localhost:[port]api/notes/filterNotes?
        public ActionResult<List<Note>> FilteredNotesByQueryParams(string? text, int? priority)
        {
            try
            {
                if (string.IsNullOrEmpty(text) && priority == null)
                {
                    return Ok(StaticDb.Notes);
                }
                if (string.IsNullOrEmpty(text))
                {
                    List<Note> notesDb = StaticDb.Notes.Where(x => (int)x.Priority == priority).ToList();
                    return Ok(notesDb);
                }
                if (priority == null)
                {
                    List<Note> notesDb = StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())).ToList();
                    return Ok(notesDb);
                }
                List<Note> filteredNotes =
                    StaticDb.Notes.Where(x => x.Text.ToLower().Contains(text.ToLower())
                    && (int)x.Priority == priority).ToList();
                return Ok(filteredNotes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpPost]
        public IActionResult AddNote([FromBody] Note note)
        {
            try
            {
                //first check negative scenarios
                if(string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("You must specify text");
                }
                if (note.Tags == null || !note.Tags.Any())
                {
                    return BadRequest("All notes must have some tags!");
                }
                //add to db
                StaticDb.Notes.Add(note);
                return StatusCode(StatusCodes.Status201Created, "Note created");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }
        [HttpPut]
        public IActionResult UpdateNote([FromBody] Note note)
        {
            try
            {
                Note noteDb = StaticDb.Notes.FirstOrDefault(x => x.Id == note.Id);
                if(noteDb == null)
                {
                    return NotFound($"Note with id {note.Id} was not found");
                }
                if (string.IsNullOrEmpty(note.Text))
                {
                    return BadRequest("You must specify text");
                }
                if (note.Tags == null || !note.Tags.Any())
                {
                    return BadRequest("You must specify tags");
                }
                noteDb.Text = note.Text;
                noteDb.Priority = note.Priority;
                noteDb.Tags = note.Tags;

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred");
            }
        }

    }
}
