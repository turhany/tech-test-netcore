using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Services
{
    public static class ApplicationDbContextConvenience
    {
        public static IQueryable<TodoList> RelevantTodoLists(this ApplicationDbContext dbContext, string userId)
        {
            return dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .Where(tl => tl.Owner.Id == userId || tl.Items.Any(ti => ti.ResponsibleParty.Id == userId));
        }

        public static TodoList SingleTodoList(this ApplicationDbContext dbContext, int todoListId, TodoItemSortByOperation todoItemSortByOperation = TodoItemSortByOperation.Importance)
        {
            var todoList = dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items.OrderBy(ti => ti.Importance))
                .ThenInclude(ti => ti.ResponsibleParty)
                .Single(tl => tl.TodoListId == todoListId);

            if (todoItemSortByOperation == TodoItemSortByOperation.Importance)
            {
                todoList.Items = todoList.Items.OrderBy(ti => ti.Importance).ToList();
            }
            else if (todoItemSortByOperation == TodoItemSortByOperation.Rank)
            {
                todoList.Items = todoList.Items.OrderByDescending(ti => ti.Rank).ToList();
            }

            return todoList;
        }

        public static TodoItem SingleTodoItem(this ApplicationDbContext dbContext, int todoItemId)
        {
            return dbContext.TodoItems.Include(ti => ti.TodoList).Single(ti => ti.TodoItemId == todoItemId);
        }
    }
}