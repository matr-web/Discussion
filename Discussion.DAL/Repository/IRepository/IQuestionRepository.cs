using Discussion.Entities;


namespace Discussion.DAL.Repository.IRepository;

public interface IQuestionRepository : IRepository<QuestionEntity>
{
    new Task Remove(QuestionEntity questionEntity);
}
