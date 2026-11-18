using System.Collections.ObjectModel;

public interface TodoRepository
{
    public Task<Todo?> Get(Guid id);

    public Task<IReadOnlyList<Todo>> GetAll();

    public Task<Todo> Create(CreateTodoRequest item);

    public Task Delete(Guid id);
}