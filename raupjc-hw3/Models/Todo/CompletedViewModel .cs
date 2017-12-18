﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raupjc_hw3.Models.Todo
{
    public class CompletedViewModel
    {
        public List<TodoViewModel> TodoViewModels { get; set; }

        public CompletedViewModel(List<TodoViewModel> todoViewModels)
        {
            TodoViewModels = todoViewModels;
        }
    }
}
