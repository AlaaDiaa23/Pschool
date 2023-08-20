using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pschool.DTO;
using Pschool.Repository.Base;
using System.Net;
using System.Numerics;

namespace Pschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly IBaseRepository<Parent> _parentRepo;

        public ParentsController(IBaseRepository<Parent>ParentRepo)
        {
            _parentRepo = ParentRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _parentRepo.GetAllAsync());
            }
            catch (Exception)
            {
                return BadRequest("error");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res=await _parentRepo.GetByIdAsync(id);
            if(res==null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Parentdto parentdto)
        {
            var parent = new Parent { 
                FirstName = parentdto.FirstName,
                LastName = parentdto.LastName,
                Address= parentdto.Address,
                HomePhone= parentdto.HomePhone,
                Phone = parentdto.Phone,
                Email= parentdto.Email,
                Sibilings = parentdto.Sibilings,
                WorkPhone = parentdto.WorkPhone,
                UserName   = parentdto.UserName,


            
            
            
            };
            await _parentRepo.CreateAsync(parent);
            return Ok(parent);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Parentdto parentdto)
        {
            var parent = await _parentRepo.GetByIdAsync(id);
            if (parent == null)
            {
                return NotFound("error");
            }
            parent.FirstName = parentdto.FirstName;
            parent.LastName = parentdto.LastName;
            parent.Address = parentdto.Address;
            parent.HomePhone = parentdto.HomePhone;
            parent.Phone = parentdto.Phone;
            parent.Email = parentdto.Email;
            parent.Sibilings = parentdto.Sibilings;
            parent.WorkPhone = parentdto.WorkPhone;
            parent.UserName = parentdto.UserName;
           await _parentRepo.UpdateAsync(parent.Id);
            return Ok(parent);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var parent = await _parentRepo.GetByIdAsync(id);
            if (parent == null)
            {
                return NotFound("error");
            }
           await _parentRepo.DeleteAsync(parent.Id);
            return Ok(parent);
        }


    }
}
