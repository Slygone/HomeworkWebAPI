using SEDC.NotesApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Dtos.Note
{
    public class NoteDto
    {
        public string Text { get; set; }
        public int Priority { get; set; }
        public int Tag { get; set; }
        public string UserFullName { get; set; }

    }
}
