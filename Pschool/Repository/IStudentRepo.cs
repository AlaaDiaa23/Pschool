using DataAccess;
using System.Reflection;

namespace Pschool.Repository
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> GetStudentsAsync(int parent=0);
        Task<Student> GetById(int id);
        Task<IEnumerable<Student>> Search(string name);
        IEnumerable<Parent> GetSelectedItems();





    }
}
