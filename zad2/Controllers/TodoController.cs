using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using zad1;


using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using zad1;

using Microsoft.AspNetCore.Authorization;

using zad2.Data;

using Microsoft.AspNetCore.Identity;

using zad2.Models.Todo;

using AutoMapper;
using zad2.Models;

namespace zad2.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            List<TodoItem> items = _repository.GetActive(new Guid(applicationUser.Id));
            //List<TodoItemLabel> labels =await _repository.getAllLabels();
            
            List<TodoViewModel> destination = Mapper.Map<List<TodoItem>, List<TodoViewModel>>(items);
            IndexViewModel indexModel = new IndexViewModel(destination);
            return View(indexModel);
        }

        public IActionResult Add()
        {
            return View();
        }

       

        public async Task<IActionResult> MarkAsCompleted(Guid Id)

        {

            ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            _repository.MarkAsCompleted(Id, new Guid(applicationUser.Id));

            return RedirectToAction("Index");



        }



        [HttpPost]

        public async Task<IActionResult> Add(AddTodoViewModel it)

        {
            

        
        if (ModelState.IsValid)

         {

             ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);

             TodoItem todo = new TodoItem(it.Text, new Guid(applicationUser.Id))

             {

                 DateDue = it.DateDue

             };

             if (it.Labels!=null)

             {


                 string[] labels = it.Labels.Split(',');

                 List<TodoItemLabel> todoItemLabels = new List<TodoItemLabel>();

                 foreach (String label in labels)

                 {
                       _repository.AddLabel(label.Trim(), todo.Id,todo.UserId);


                 }

             }

             _repository.Add(todo);

             return RedirectToAction("Index");

         }

         return View();
         

    }



        public async Task<IActionResult> Completed()

        {

            ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            List<TodoItem> items =await _repository.GetCompleted(new Guid(applicationUser.Id));
            //List<TodoItemLabel> labels = await _repository.getAllLabels();
            List<TodoViewModel> destination = Mapper.Map<List<TodoItem>, List<TodoViewModel>>(items);
            CompletedViewModel completedModel = new CompletedViewModel(destination);
           

            return View(completedModel);

        }



        

        public async Task<IActionResult> RemoveFromCompleted(Guid Id)

        {

            ApplicationUser applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            _repository.Remove(Id, new Guid(applicationUser.Id));

            return RedirectToAction("Index");

        }

    }
}
