using System.Collections.Generic;
using System.Linq;
using Interfaces.Data;
using Interfaces.Providers;

namespace Model.Providers
{
    public class QuestionsAssetsProvider : IQuestionsAssetsProvider
    {
        private readonly IQuestionsContainerAsset _questionsContainerAsset;

        public IQuestionAsset RootQuestion => _questionsContainerAsset.RootQuestion;
        public IQuestionAsset[] AllQuestions => _questionsContainerAsset.AllQuestionAssets;
        public IReadOnlyDictionary<string, IQuestionAsset> QuestionsById { get; }

        public QuestionsAssetsProvider(IQuestionsContainerAsset questionsContainerAsset)
        {
            _questionsContainerAsset = questionsContainerAsset;
            QuestionsById = questionsContainerAsset.AllQuestionAssets.ToDictionary(asset => asset.QuestionId, asset => asset);
        }
    }
}