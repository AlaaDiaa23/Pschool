using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApiwebsite.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiwebsite.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly HttpClient client;


        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5069/");
        }

        public async Task<IActionResult> Index()
        {

           List<Student> students = new List<Student>();


            HttpResponseMessage response = await client.GetAsync("/api/Students");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<Student>>(result);
            }

            return View(students);
        }
     
        public async Task<IActionResult> Details(int id)
        {
            Student student = new Student();
            
            HttpResponseMessage response = await client.GetAsync($"/api/Students/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<Student>(result);
            }

            return View(student);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            
            HttpResponseMessage response = await client.DeleteAsync($"/api/Students/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

       


        public async void SelectedItems(int selectedid = 1)
        {

         
            HttpResponseMessage response = await client.GetAsync("/api/Parents");

            var result = response.Content.ReadAsStringAsync().Result;
            List<Parent> Parents = JsonConvert.DeserializeObject<List<Parent>>(result);

            SelectList selectListItems = new SelectList(Parents, "Id", "FirstName", selectedid);
            ViewBag.SelectListItems = selectListItems;
        }
        public async Task<IActionResult> Create()
        {
         
            HttpResponseMessage response = await client.GetAsync("/api/Parents");

            var result = response.Content.ReadAsStringAsync().Result;
            List<Parent> Parents = JsonConvert.DeserializeObject<List<Parent>>(result);

            SelectList selectListItems = new SelectList(Parents, "Id", "FirstName");
            ViewBag.SelectListItems = selectListItems;

            return View();

        }
        
     
      
        [HttpPost]
        public async Task<IActionResult> Create(Student s)
        {
          
            SelectedItems(s.Id);

            var response = await client.PostAsJsonAsync("/api/Students", s);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(s);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
        
            Student student = new Student();
           
            HttpResponseMessage response = await client.GetAsync($"/api/Students/{id}");
            HttpResponseMessage responseParent = await client.GetAsync("/api/Parents");

            var result_parent = responseParent.Content.ReadAsStringAsync().Result;
            List<Parent> Parents = JsonConvert.DeserializeObject<List<Parent>>(result_parent);

            SelectList selectListItems = new SelectList(Parents, "Id", "FirstName");
            ViewBag.SelectListItems = selectListItems;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<Student>(result);
            }

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student s)
        {
          
            var response = await client.PutAsJsonAsync($"/api/Students/{s.Id}", s);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            SelectedItems(s.Id);
            return View();
        }
    

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private static  List<Student> SearchCustomers(string name)
        {
            List<Student> Students = new List<Student>();

            HttpClient clients = new HttpClient();
            clients.BaseAddress = new Uri("http://localhost:5069/");

            HttpResponseMessage responses =  clients.GetAsync(string.Format("/Search?name={0}", name)).Result;
            if (responses.IsSuccessStatusCode)
            {
                Students = JsonConvert.DeserializeObject<List<Student>>(responses.Content.ReadAsStringAsync().Result);
            }

            return Students;
        }
        
    }
}

