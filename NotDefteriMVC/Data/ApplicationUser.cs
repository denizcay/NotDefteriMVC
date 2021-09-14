using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotDefteriMVC.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegisterationTime { get; set; } = DateTime.Now;

        [MaxLength(225)]
        public string PhotoPath { get; set; }

        public List<Note> Notes { get; set; }
    }
}
