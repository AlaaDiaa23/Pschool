using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pschool.DTO
{
    public class Parentdto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Phone]
        public string? HomePhone { get; set; }
        public string? WorkPhone { get; set; }


        public int Sibilings { get; set; }
    }
}
