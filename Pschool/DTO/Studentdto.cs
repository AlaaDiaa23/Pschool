using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pschool.DTO
{
    public class Studentdto
    {
        [Display(Name = "Student Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string Address { get; set; }
        public int Year_Group { get; set; }
        public string? Grade { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }
        public int ParentId { get; set; }
    }
}
