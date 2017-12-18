using System;

using System.Collections.Generic;

using System.Data.Entity;

using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using System.Threading.Tasks;

namespace zad1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _base;

        public TodoSqlRepository(TodoDbContext context)
        {
            _base = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (_base.TodoItems.Any(t => t.Id == todoItem.Id))

            {

                throw new DuplicateTodoItemException("Duplicate!");

            }
            else

            {




                _base.TodoItems.Add(todoItem);

                _base.SaveChanges();

            }
        }

        public void AddLabel(string text, Guid todoId,Guid userId)

        {

            var item = _base.TodoItems.FirstOrDefault(s=>s.Id.Equals(todoId) && s.UserId.Equals(userId));

            var label =  _base.TodoItemLabels.FirstOrDefault(s => s.Value == text);



            if (item != null)

            {

                if (label == null)

                {

                    label =new TodoItemLabel(text);

                    _base.TodoItemLabels.Add(label);

                }



                label.LabelTodoItems.Add(item);

                item.Labels.Add(label);
                _base.SaveChanges();

            }





        }

        public async Task<TodoItem> Get(Guid todoId, Guid userId)
        {
            TodoItem item = await _base.TodoItems.FirstOrDefaultAsync(s => s.Id.Equals(todoId));
            if (!item.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("AccessDenied");
            }
            return item;
        }
        public async Task<List<TodoItemLabel>> getAllLabels()
        {
            return await _base.TodoItemLabels.ToListAsync();
        }

        public List<TodoItem> GetActive(Guid userId)
        {

            return _base.TodoItems.Include(t => t.Labels).Where(t => !t.IsCompleted && t.UserId == userId).ToList(); ;
        }

        public async Task<List<TodoItem>> GetAll(Guid userId)
        {
            List<TodoItem> list = await _base.TodoItems.Where(s => s.UserId.Equals(userId)).OrderByDescending(s => s.DateCreated).ToListAsync();
            return list;
        }

        public async Task<List<TodoItem>> GetCompleted(Guid userId)
        {
            List<TodoItem> list = await _base.TodoItems.Include(t => t.Labels).Where(s => s.UserId.Equals(userId) && s.IsCompleted).ToListAsync();
            return list;
        }

        public async Task<List<TodoItem>> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return await _base.TodoItems.Where(s => filterFunction(s) && s.UserId.Equals(userId)).ToListAsync();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem todoItem = _base.TodoItems.FirstOrDefault(t => t.Id == todoId && t.UserId == userId);

            if (todoItem != null)

            {

                if (todoItem.MarkAsCompleted())

                {
                    _base.Entry(todoItem).State = EntityState.Modified;
                    _base.SaveChanges();

                    return true;

                }

                else

                {

                    return false;

                }

            }

            else

            {

                throw new TodoAccessDeniedException("AccessDenied");

            }
        }

        public async Task<bool> Remove(Guid todoId, Guid userId)
        {
            TodoItem item = await Get(todoId, userId);
            if (item != null)
            {
                _base.TodoItems.Remove(item);
                _base.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }



        public async void Update(TodoItem todoItem, Guid userId)
        {


            TodoItem item = await Get(todoItem.Id, userId);

            if (item == null)
            {
                Add(todoItem);

            }
            else
            {
                _base.Entry(item).State = EntityState.Modified;
                item = todoItem;
            }
            _base.SaveChanges();
        }

    }

    [Serializable]
    internal class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException()
        {
        }

        public TodoAccessDeniedException(string message) : base(message)
        {
        }

        public TodoAccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TodoAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException()
        {
        }

        public DuplicateTodoItemException(string message) : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Guid id) : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateTodoItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
