using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class Parent
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        [Phone]
        public string ?Phone { get; set; }

        [Phone]
        public string? HomePhone { get; set; }
        public string? WorkPhone { get; set; }

        [Range(0,10)]
        public int Sibilings { get; set; }
        public List<Student> Students { get; set; }
    }
}
