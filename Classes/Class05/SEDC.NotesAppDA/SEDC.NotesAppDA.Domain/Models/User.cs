﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppDA.Domain.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }
        [MaxLength(50)]
        public string? UserName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [InverseProperty("User")]//always use inversproperty for lists
        public List<Note> Notes { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
