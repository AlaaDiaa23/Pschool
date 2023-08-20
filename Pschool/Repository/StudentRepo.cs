using DataAccess;
using Microsoft.EntityFrameworkCore;
using Pschool.Data;
using System.Reflection;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Pschool.Repository.Base;

namespace Pschool.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IBaseRepository<Student> s;
        private readonly IBaseRepository<Parent> p;

        public StudentRepo(ApplicationDbContext db,IBaseRepository<Student>s,IBaseRepository<Parent>p)
        {
            this.db = db;
            this.s = s;
            this.p = p;
        }
      public  async Task<IEnumerable<Student>> GetStudentsAsync(int parentid=0)
        {
           
                return await db.Students
                    .Where(m => m.ParentId == parentid || parentid == 0)
                    .Include(m => m.Parent)
                    .ToListAsync();
            
        }
        public async Task<Student> GetById(int id)
        {
            return await db.Students.Include(m => m.Parent).SingleOrDefaultAsync(m => m.Id == id);
        }
        [HttpPost]
        public async Task<IEnumerable<Student>> Search(string name)
        {
            IQueryable<Student> query = db.Students;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Parent.FirstName.Contains(name)
                            || e.Parent.LastName.Contains(name));
            }

           

            return await query.ToListAsync();
        }
     
        public IEnumerable<Parent> GetSelectedItems()
        {
            var selectedItems = db.Parents
                        .Select(item => new Parent
                        {
                            Id = item.Id,
                            FirstName = item.FirstName
                        })
                        .ToList();

            return selectedItems;
        }
      
    }
    
}
