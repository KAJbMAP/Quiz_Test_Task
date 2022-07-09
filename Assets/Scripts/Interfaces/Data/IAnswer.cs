namespace Interfaces.Data
{
    public interface IAnswer
    {
        string AnswerText { get; }
        bool IsCorrectAnswer { get; }
    }
}