using System;
using Interfaces.Data;

namespace Interfaces.Model.Systems
{
    public interface IAnswerValidationSystem
    {
        IQuestionAsset CurrentQuestion { get; }
        void PickQuestion(IQuestionAsset questionAsset);
        bool ValidateAnswer(IAnswer answer);
        event Action<IQuestionAsset, bool> QuestionAnswered;
    }
}