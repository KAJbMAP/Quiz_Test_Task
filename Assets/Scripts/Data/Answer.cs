using Interfaces.Data;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class Answer : IAnswer
    {
        [SerializeField] private string _answerText;
        [SerializeField] private bool _isCorrectAnswer;
        
        public string AnswerText => _answerText;
        public bool IsCorrectAnswer => _isCorrectAnswer;
    }
}