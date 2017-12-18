using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace raupjc_hw3.Models.Todo
{
    public class AddTodoViewModel
    {
        [Required, MinLength(3)]
        public string Text { get; set; }
        [Required]
        public DateTime? DateDue { get; set; }
        [Required]
        public string Labels { get; set; }
    }
}
