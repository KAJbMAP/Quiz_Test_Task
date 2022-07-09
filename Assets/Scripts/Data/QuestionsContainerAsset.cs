using Interfaces.Data;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "QuestionsContainer", menuName = "Quiz/Question container asset", order = 0)]
    public class QuestionsContainerAsset : ScriptableObject, IQuestionsContainerAsset
    {
        [Header("Root question")]
        [SerializeField] private QuestionAsset _rootQuestion;
        [Header("All questions")]
        [SerializeField] private QuestionAsset[] _allQuestionAssets;

        public IQuestionAsset RootQuestion => _rootQuestion;

        public IQuestionAsset[] AllQuestionAssets => _allQuestionAssets;
    }
}