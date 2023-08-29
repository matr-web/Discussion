using Discussion.Entities;

namespace Discussion.DAL.Repository.IRepository;

/// <summary>
/// In IAnswerRepository You already get the base methods from IRepository where entity will be 'AnswerEntity'.
/// </summary>
public interface IAnswerRepository : IRepository<AnswerEntity>
{
    /// You can add more method's here if it is necessary.
    /// Those will be accessible only through this Interface.    
}
