using System.ComponentModel.DataAnnotations;

namespace Discussion.Entities;

/// <summary>
/// User Entity.
/// </summary>
public class UserEntity
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// User Name.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    /// <summary>
    /// Role.
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// PasswordHash.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Collection of User's Question's.
    /// </summary>
    public virtual ICollection<QuestionEntity> Questions { get; set; }

    /// <summary>
    /// Collection of User's Answer's.
    /// </summary>
    public virtual ICollection<AnswerEntity> Answers { get; set; }

    /// <summary>
    /// Collection of User's Ratings's.
    /// </summary>
    public virtual ICollection<RatingEntity> Ratings { get; set; }
}
