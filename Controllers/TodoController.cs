using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly HybridCache _cache;
    private readonly TodoRepository _repo;
    public TodoController(HybridCache cache, TodoRepository repo)
    {
        _cache = cache;
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _repo.GetAll();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await _cache.GetOrCreateAsync(id.ToString(), async token => await _repo.Get(id));
        return item is not null ? 
            Ok(item)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoRequest todo)
    {
        var created = await _repo.Create(todo);
        return created is not null ? 
        Ok(created)
        : Conflict();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repo.Delete(id);
        return NoContent();
    }
}