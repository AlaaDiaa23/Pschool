using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class Student
    {
        public int Id { get; set; }
        [Display(Name = "Student Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        

        public string Address { get; set; }
        public int Year_Group { get; set; }
        public string? Grade { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; } 
    }
}
