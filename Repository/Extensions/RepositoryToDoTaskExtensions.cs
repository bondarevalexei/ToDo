using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryToDoTaskExtensions
{
    public static IQueryable<ToDoTask> Search(this IQueryable<ToDoTask> toDoTasks,
        string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return toDoTasks;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return toDoTasks.Where(todo => todo.Title.ToLower()
                                          .Contains(lowerCaseTerm));
    }

    public static IQueryable<ToDoTask> Sort(this IQueryable<ToDoTask> toDos, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return toDos.OrderBy(t => t.Title);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<ToDoTask>(orderByQueryString);

        if (string.IsNullOrEmpty(orderQuery))
            return toDos.OrderBy(t => t.Title);

        return toDos.OrderBy(orderQuery);
    }
}
