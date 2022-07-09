using System.Collections.Generic;
using Enums;
using Interfaces.Data;
using Misc;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Question", menuName = "Quiz/Question asset", order = 0)]
    public class QuestionAsset : ScriptableObject, IQuestionAsset
    {
        [Header("Tile neighbours")]
        [SerializeField] private TileChilds _tileChilds;
        [SerializeField] private QuestionAsset _leftChild;
        [SerializeField] private QuestionAsset _topChild;
        [SerializeField] private QuestionAsset _rightChild;
        [SerializeField] private QuestionAsset _bottomChild;

        [Header("Question data")]
        [SerializeField] private string _questionId;
        [SerializeField] private QuestionCategoryAsset _questionCategoryAsset;
        [SerializeField] private QuestionInfo _questionInfo;

        public TileChilds TileChilds => _tileChilds;
        public string QuestionId => _questionId;
        public IQuestionCategoryInfo CategoryInfo => _questionCategoryAsset.QuestionCategoryInfo;
        public IQuestionInfo QuestionInfo => _questionInfo;

        public IEnumerable<(TileChilds tileChildDirection, IQuestionAsset questionAsset)> QuestionChilds
        {
            get
            {
                if (FlagsHelper.IsSet(_tileChilds, TileChilds.Left)) yield return (TileChilds.Left, _leftChild);
            
                if (FlagsHelper.IsSet(_tileChilds, TileChilds.Top)) yield return (TileChilds.Top, _topChild);
            
                if (FlagsHelper.IsSet(_tileChilds, TileChilds.Right)) yield return (TileChilds.Right, _rightChild);
            
                if (FlagsHelper.IsSet(_tileChilds, TileChilds.Bottom)) yield return (TileChilds.Bottom, _bottomChild);
            }
        }
    }
}