using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zad1;

namespace raupjc_hw3.Models.Todo
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public TodoViewModel(string text, DateTime dateCreated)
        {
            // Generates new unique identifier
            Id = Guid.NewGuid();
            // DateTime .Now returns local time , it wont always be what you expect(depending where the server is).
            // We want to use universal (UTC ) time which we can easily convert to local when needed.
            // ( usually done in browser on the client side )
            DateCreated = DateTime.UtcNow;
            Text = text;
        }

        // Shorter syntax for single line getters in C#6
        // public bool IsCompleted = > DateCompleted . HasValue ;
        public bool IsCompleted
        {
            get { return DateCompleted.HasValue; }
        }

        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public override bool Equals(object o)
        {
            if (!(o is TodoViewModel))
            {
                return false;
            }
            return this.Id == ((TodoViewModel)o).Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary >
        /// User id that owns this TodoItem
        /// </ summary >
        public Guid UserId { get; set; }
        /// <summary >

        /// List of labels associated with TodoItem
        /// </ summary >
        public List<TodoViewModel> Labels { get; set; }
        /// <summary >
        /// Date due . If null , no date was set by the user
        /// </ summary >
        public DateTime? DateDue { get; set; }
        public TodoViewModel(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
           // Labels = new List<TodoItemLabel>();
        }
        public TodoViewModel()
        {
            // entity framework needs this one
            // not for use :)
        }
    }
}
