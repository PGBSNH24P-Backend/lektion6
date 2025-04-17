using Microsoft.EntityFrameworkCore;

namespace ASP.NET;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = Environment.GetEnvironmentVariable("LEKTION6_CONNECTION_STRING");
        if (connectionString == null)
        {
            Console.WriteLine("Connection string is missing!");
            return;
        }

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // builder.Services.AddDbContext<AppDbContext>(options =>
        // options.UseNpgsql("Host=localhost;Port=5432;Database=lektion6;Username=postgres;Password=password"));

        builder.Services.AddControllers();
        builder.Services.AddScoped<TodoService>();
        builder.Services.AddScoped<TodoRepository>();

        var app = builder.Build();

        CreateTags(app);
        TestEF(app);

        app.MapControllers();

        app.Run();
    }

    static void CreateTags(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        CreateTag(context, "education");
        CreateTag(context, "entertainment");
        CreateTag(context, "home");
        CreateTag(context, "work");
    }

    static void CreateTag(AppDbContext context, string tagName)
    {
        var existingTag = context.Tags
            .Where(tag => tag.Name.Equals(tagName))
            .FirstOrDefault();
        if (existingTag == null)
        {
            context.Tags.Add(new TagEntity(tagName));
            context.SaveChanges();
        }
    }

    static void TestEF(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        /*context.Posts.Add(new PostEntity("A title", "Some content"));
        context.Posts.Add(new PostEntity("Avengers!", "Some content"));
        context.Posts.Add(new PostEntity("Gaming!", "Some content"));
        context.SaveChanges();*/


        /*var avengersPost = context.Posts.Where(post => post.Title.Equals("Avengers!")).FirstOrDefault();
        if (avengersPost != null)
        {
            var firstComment = new CommentEntity("Very fun movie!", avengersPost);
            avengersPost.Comments.Add(firstComment);
            context.Comments.Add(firstComment);

            var secondComment = new CommentEntity("Awesome!!", avengersPost);
            avengersPost.Comments.Add(secondComment);
            context.Comments.Add(secondComment);

            context.SaveChanges();
        }*/

        var avengersPost = context.Posts
            .Include(model => model.Comments)
            //.ThenInclude(model => model.User)
            .Where(post => post.Title.Equals("Avengers!")).FirstOrDefault();
        if (avengersPost != null)
        {
            Console.WriteLine(avengersPost.Comments.Count);
            foreach (var comment in avengersPost.Comments)
            {
                Console.WriteLine($"- {comment.Content}");
            }
        }
    }
}
