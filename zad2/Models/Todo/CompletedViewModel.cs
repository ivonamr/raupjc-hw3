using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zad1;

namespace zad2.Models.Todo
{
    public class CompletedViewModel
    {
        private List<TodoItemLabel> labels;

        public List<TodoViewModel> TodoViewModels { get; set; }



        public CompletedViewModel(List<TodoViewModel> todoViewModels)

        {

            TodoViewModels = todoViewModels;

        }

        public CompletedViewModel()
        {
        }

        public CompletedViewModel(List<TodoViewModel> todoViewModels, List<TodoItemLabel> labels) : this(todoViewModels)
        {
            this.labels = labels;
        }
    }
}
