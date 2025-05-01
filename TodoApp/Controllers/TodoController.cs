using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var todoItems = _context.TodoItems.ToList();
            return View(todoItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.TodoItems.Add(todoItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoItem);
        }

        public IActionResult Edit(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(i => i.Id == id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem todoItem)
        {
            Console.WriteLine($"DEBUG: IsCompleted = {todoItem.IsCompleted}");

            if (ModelState.IsValid)
            {
                _context.TodoItems.Update(todoItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoItem);
        }



        public IActionResult Delete(int id)
        {
            var itemToDelete = _context.TodoItems.FirstOrDefault( d => d.Id == id);
            if (itemToDelete != null)
            {
                _context.TodoItems.Remove(itemToDelete);
                _context.SaveChanges(); 
            }

            return RedirectToAction("Index");
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
