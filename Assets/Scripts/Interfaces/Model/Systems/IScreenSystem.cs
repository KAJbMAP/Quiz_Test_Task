using System;
using Enums;

namespace Interfaces.Model.Systems
{
    public interface IScreenSystem
    {
        QuizScreen CurrentQuizScreen { get; }
        void SwitchScreen(QuizScreen quizScreen);
        event Action<QuizScreen> ScreenChanged;
    }
}