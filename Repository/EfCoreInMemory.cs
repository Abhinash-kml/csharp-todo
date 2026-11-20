using Microsoft.EntityFrameworkCore;

public class EfCoreInMemoryRepository : TodoRepository
{
    private readonly AppDbContext _context; 
    public EfCoreInMemoryRepository()
    {
        _context = new AppDbContext();
        _context.Database.EnsureCreatedAsync();
    }

    public async Task<Todo?> Get(Guid id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<IReadOnlyList<Todo>> GetAll()
    {
       return _context.Todos.ToList<Todo>();
    }

    public async Task<Todo> Create(CreateTodoRequest item)
    {
        var addded = new Todo
        {
            Name = item.Name,
            IsCompleted = item.IsCompleted
        };
        await _context.Todos.AddAsync(addded);
        await _context.SaveChangesAsync();
        return addded;
    }

    public async Task Delete(Guid id)
    {
        var range = _context.Todos.Where(item => item.Id == id);
        _context.RemoveRange(range);
        await _context.SaveChangesAsync();
    }
}