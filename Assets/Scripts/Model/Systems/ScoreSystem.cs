using System;
using Interfaces.Data;
using Interfaces.Model.Systems;

namespace Model.Systems
{
    public class ScoreSystem : IScoreSystem, IDisposable
    {
        private const int PointsByCorrectAnswer = 10;
        private readonly IAnswerValidationSystem _answerValidationSystem;
        
        public int CurrentScore { get; private set; }

        public event Action<int> ScoreUpdated;

        public ScoreSystem(IAnswerValidationSystem answerValidationSystem)
        {
            _answerValidationSystem = answerValidationSystem;
            _answerValidationSystem.QuestionAnswered += OnAnswerValidationSystemQuestionAnswered;
        }

        public void Dispose()
        {
            _answerValidationSystem.QuestionAnswered -= OnAnswerValidationSystemQuestionAnswered;
        }
        
        private void OnAnswerValidationSystemQuestionAnswered(IQuestionAsset questionAsset, bool isAnsweredCorrect)
        {
            if (!isAnsweredCorrect) return;

            CurrentScore += PointsByCorrectAnswer;
            ScoreUpdated?.Invoke(CurrentScore);
        }
    }
}