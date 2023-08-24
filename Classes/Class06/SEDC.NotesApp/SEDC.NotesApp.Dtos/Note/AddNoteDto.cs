using SEDC.NotesApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Dtos.Note
{
    public class AddNoteDto
    {
        public string Text {  get; set; }
        public int UserId { get; set; }
        public Tag Tag { get; set; }
        public Priority Priority { get; set; }

    }
}
