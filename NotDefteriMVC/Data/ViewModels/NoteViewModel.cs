using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotDefteriMVC.Data.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }
    }
}
