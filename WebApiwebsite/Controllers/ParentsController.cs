using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApiwebsite.Models;

namespace WebApiwebsite.Controllers
{
    public class ParentsController : Controller
    {
        private readonly ILogger<ParentsController> _logger;
        private readonly HttpClient client;


        public ParentsController(ILogger<ParentsController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5069/");
        }

        public async Task< IActionResult> Index()
        {
            List<Parent>parents = new List<Parent>();   
         
            HttpResponseMessage response = await client.GetAsync("/api/Parents");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                parents= JsonConvert.DeserializeObject<List<Parent>>(result);   
            }

            return View(parents);
        }
        public async Task<IActionResult> Details(int id)
        {
            Parent parents = new Parent();
    
            HttpResponseMessage response = await client.GetAsync($"/api/Parents/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                parents = JsonConvert.DeserializeObject<Parent>(result);
            }

            return View(parents);
        }
        public async Task<IActionResult> Delete(int id)
        {
       
            HttpResponseMessage response = await client.DeleteAsync($"/api/Parents/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Parent p)
        {
       
            var response = await client.PostAsJsonAsync("/api/Parents",p);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task< IActionResult> Edit(int id)
        {
            Parent parents = new Parent();
          
            HttpResponseMessage response = await client.GetAsync($"/api/Parents/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                parents = JsonConvert.DeserializeObject<Parent>(result);
            }

            return View(parents);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Parent p)
        {
           
            var response = await client.PutAsJsonAsync($"/api/Parents/{p.Id}", p);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

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
    }
}