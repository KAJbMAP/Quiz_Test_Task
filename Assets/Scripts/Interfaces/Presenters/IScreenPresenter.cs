using Enums;

namespace Interfaces.Presenters
{
    public interface IScreenPresenter
    {
        QuizScreen QuizScreen { get; }
        void Present();
        void Hide();
    }
}