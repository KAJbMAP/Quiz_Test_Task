using System;
using Interfaces.Data;
using Interfaces.Model.Systems;

namespace Model.Systems
{
    public class AnswerValidationSystem : IAnswerValidationSystem
    {
        public IQuestionAsset CurrentQuestion { get; private set; }

        public void PickQuestion(IQuestionAsset questionAsset)
        {
            CurrentQuestion = questionAsset;
        }

        public bool ValidateAnswer(IAnswer answer)
        {
            var isCorrectAnswer = answer.IsCorrectAnswer;
            QuestionAnswered?.Invoke(CurrentQuestion, isCorrectAnswer);
            return isCorrectAnswer;
        }

        public event Action<IQuestionAsset, bool> QuestionAnswered;
    }
}