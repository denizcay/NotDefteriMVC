using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotDefteriMVC.Data.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }
    }
}
