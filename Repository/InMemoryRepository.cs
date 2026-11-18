using System.Collections.ObjectModel;

public class InMemoryTodoRepository : TodoRepository
{
    private readonly List<Todo> Items;

    public InMemoryTodoRepository()
    {
        Items = [];
    }

    public async Task<Todo?> Get(Guid id)
    {
        var item = Items.FirstOrDefault(i => i.Id == id);
        return item;
    }

    public async Task<IReadOnlyList<Todo>> GetAll()
    {
        var items = Items.AsReadOnly();
        return items;
    }

    public async Task<Todo> Create(CreateTodoRequest item)
    {
        var todo = new Todo
        {
            Name = item.Name,
            IsCompleted = item.IsCompleted
        };
        Items.Add(todo);
        return todo;
    }

    public async Task Delete(Guid id)
    {
        Items.RemoveAll(pred => pred.Id == id);
    }

}