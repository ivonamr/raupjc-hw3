using System;

using System.Collections.Generic;

using System.Data.Entity;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace zad1
{
    public class TodoDbContext : DbContext
{
    public IDbSet<TodoItem> TodoItems { get; set; }
    public IDbSet<TodoItemLabel> TodoItemLabels { get; set; }



    public TodoDbContext(string cnnstr) : base(cnnstr)
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>().HasKey(s => s.Id);
        modelBuilder.Entity<TodoItem>().Property(s => s.DateCreated).IsRequired();
        modelBuilder.Entity<TodoItem>().Property(s => s.IsCompleted).IsRequired();
        modelBuilder.Entity<TodoItem>().Property(s => s.Text).IsRequired();
        modelBuilder.Entity<TodoItem>().Property(s => s.UserId).IsRequired();
        modelBuilder.Entity<TodoItem>().Property(s => s.DateCompleted).IsOptional();
        modelBuilder.Entity<TodoItem>().Property(s => s.DateDue).IsOptional();

        modelBuilder.Entity<TodoItemLabel>().HasKey(c => c.Id);
        modelBuilder.Entity<TodoItemLabel>().Property(c => c.Value).IsRequired();


        modelBuilder.Entity<TodoItem>().HasMany(s => s.Labels).WithMany(m => m.LabelTodoItems);
    }
}
}
