using Interfaces.Data;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class QuestionInfo : IQuestionInfo
    {
        [SerializeField] private Sprite _questionSprite;
        [SerializeField] private string _question; 
        [SerializeField] private Answer[] _answers; 

        public Sprite QuestionSprite => _questionSprite;
        public string Question => _question;
        public IAnswer[] Answers => _answers;
    }
}