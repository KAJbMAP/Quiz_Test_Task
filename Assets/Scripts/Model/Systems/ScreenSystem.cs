using System;
using Enums;
using Interfaces.Model.Systems;

namespace Model.Systems
{
    public class ScreenSystem : IScreenSystem
    {
        private const QuizScreen DefaultScreen = QuizScreen.QuestionsMap;

        public QuizScreen CurrentQuizScreen { get; private set; } = DefaultScreen;
        public event Action<QuizScreen> ScreenChanged;

        public void SwitchScreen(QuizScreen quizScreen)
        {
            if (CurrentQuizScreen == quizScreen) return;
            
            CurrentQuizScreen = quizScreen;
            ScreenChanged?.Invoke(quizScreen);
        }
    }
}