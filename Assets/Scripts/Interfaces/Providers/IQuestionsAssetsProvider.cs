using System.Collections.Generic;
using Interfaces.Data;

namespace Interfaces.Providers
{
    public interface IQuestionsAssetsProvider
    {
        IQuestionAsset RootQuestion { get; }
        IQuestionAsset[] AllQuestions { get; }
        IReadOnlyDictionary<string, IQuestionAsset> QuestionsById { get; }
    }
}