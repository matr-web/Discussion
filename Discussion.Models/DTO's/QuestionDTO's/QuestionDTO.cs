namespace Discussion.Models.DTO_s.QuestionDTO_s;

public class QuestionDTO
{
    public static QuestionDTO ToQuestionDTO(object obj)
    {
        Type type = obj.GetType();
        var properties = type.GetProperties();

        return new QuestionDTO
        {

        };
    }
}
