using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using zad1;



namespace zad2.Models.Todo

{

    public class TodoViewModel

    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public String Text { set; get; }

        public bool IsCompleted
        {
            get

            {

                return DateCompleted.HasValue;

            }

        }

        public DateTime? DateCompleted { get; set; }

       

        //public bool HasDate { get; set; }

        public string getDate()

        {

            if (IsCompleted)

            {

                if (DateCompleted.HasValue)

                {

                    //HasDate = true;

                    return ((DateTime)DateCompleted).ToShortDateString();

                }

                else

                {

                    //HasDate = false;

                    return "";

                }

            }

            else

            {

                if (DateDue.HasValue)

                {

                    //HasDate = true;

                    return ((DateTime)DateDue).ToShortDateString();

                }

                else

                {

                    //HasDate = false;

                    return "";

                }

            }

        }

        public DateTime DateCreated { get; set; }

        

        public DateTime? DateDue { get; set; }

       

        public List<TodoItemLabel> Labels { get; set; }

        public bool MarkAsCompleted()

        {

            if (!IsCompleted)

            {

                DateCompleted = DateTime.Now;

                return true;

            }

            return false;

        }

        public string DaysUntilDue
        {

            get

            {

                if (DateDue.HasValue && !IsCompleted)

                {

                    int days = ((DateTime)DateDue - DateTime.Now).Days;

                    if (days > 0)

                    {

                        return String.Format("(za {0} dana!)",days.ToString());

                    }
                    if (days == 0) {
                        return "(Today!)";
                    }
                    else

                    {

                        return "(The deadline has passed!)";

                    }

                }
                else

                {

                    return "";

                }

            }

        }



        public TodoViewModel(string text, Guid userId)

        {

            Id = Guid.NewGuid();

            Text = text;

            DateCreated = DateTime.UtcNow;

            UserId = userId;

            Labels = new List<TodoItemLabel>();

        }



        public TodoViewModel(string text)

        {

            // Generates new unique identifier

            Id = Guid.NewGuid();

            // DateTime .Now returns local time , it wont always be what you expect (depending where the server is).

            // We want to use universal (UTC ) time which we can easily convert to local when needed.

            // ( usually done in browser on the client side )

            DateCreated = DateTime.UtcNow;

            Text = text;

        }



        public override bool Equals(Object obj)

        {

            if (obj == null || GetType() != obj.GetType())

            {

                return false;

            }



            TodoItem todoItem = (TodoItem)obj;

            return Id == todoItem.Id;

        }



        public override int GetHashCode()

        {

            return Id.GetHashCode();

        }

    }

}