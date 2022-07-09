using Interfaces.Data;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Category", menuName = "Quiz/Category asset", order = 0)]
    public class QuestionCategoryAsset : ScriptableObject, IQuestionCategoryAsset
    {
        [SerializeField] private QuestionCategoryInfo questionCategoryInfo;

        public IQuestionCategoryInfo QuestionCategoryInfo => questionCategoryInfo;
    }
}