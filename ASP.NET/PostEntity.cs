using System.ComponentModel.DataAnnotations;

public class PostEntity
{

    [Key]
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }

    public ICollection<CommentEntity> Comments { get; set; }

    public PostEntity(string title, string content)
    {
        this.Id = Guid.NewGuid();
        this.Title = title;
        this.Content = content;
        this.Comments = new List<CommentEntity>();
    }

    public PostEntity()
    {
        this.Title = string.Empty;
        this.Content = string.Empty;
        this.Comments = new List<CommentEntity>();
    }
}