
using raupjc_hw3.Models.Todo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace raupjc_hw3.Models.Todo
{
    public class IndexViewModel
    {
        public List<TodoViewModel> TodoViewModels { get; set; }

        public IndexViewModel(List<TodoViewModel> todoViewModels)
        {
            TodoViewModels = todoViewModels;
        }
    }
}
