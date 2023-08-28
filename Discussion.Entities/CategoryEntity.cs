namespace Discussion.Entities;

/// <summary>
/// Category Entity.
/// </summary>
public class CategoryEntity
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Collection of Questions of given Category.
    /// </summary>
    public ICollection<QuestionEntity>? Questions { get; set; }
}
