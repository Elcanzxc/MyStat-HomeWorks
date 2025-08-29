

namespace WhoWantToBeAMillionaire_2._0.Entities;

public class Question
{
    public int Id { get; set; }
    public int Difficulty { get; set; }
    public string QuestionText { get; set; }
    public string CorrectAnswer { get; set; }
    public string IncorrectAnswer1 { get; set; }
    public string IncorrectAnswer2 { get; set; }
    public string IncorrectAnswer3 { get; set; }

}
