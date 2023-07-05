using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TodoApp.Frontend.Models;
using Dapr.Client;

namespace TodoApp.Frontend.Controllers
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var re = await client.GetAsync($"http://localhost:{port}/v1.0/invoke/todo-back/method/todos");
            var text = await re.Content.ReadAsStringAsync();
            ViewBag.Text = text + "," + re.StatusCode + ",";
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
