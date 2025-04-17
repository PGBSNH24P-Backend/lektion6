// Vanligvis skapar vi interfaces, men även här, för att spara på tid, skippar vi detta
using Microsoft.EntityFrameworkCore;

public class TodoRepository
{

    private readonly AppDbContext context;

    public TodoRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<TagEntity?> FindTagByName(string name)
    {
        return await context.Tags.Where(tag => tag.Name.Equals(name)).FirstOrDefaultAsync();
    }

    public async Task AddTodo(TodoEntity entity)
    {
        await context.Todos.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<TodoEntity>> GetTodos()
    {
        return await context.Todos
            .Include(model => model.Tags)
            .ToListAsync();
    }
}