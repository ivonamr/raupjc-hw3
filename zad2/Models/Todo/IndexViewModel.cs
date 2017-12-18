using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zad1;
using zad2.Models;

namespace zad2.Models.Todo
{
    public class IndexViewModel
    {
        public List<TodoViewModel> TodoViewModels { get; set; }
        public List<TodoItemLabel> AllLabels { get; set; }

        public IndexViewModel(List<TodoViewModel> todoViewModels)

        {

            TodoViewModels = todoViewModels;
          
           

        }
    }
}
