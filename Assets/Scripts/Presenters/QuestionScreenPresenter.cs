using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using Interfaces.Model.Systems;
using Interfaces.Presenters;
using Interfaces.View;
using Zenject.ObjectsPool;

namespace Presenters
{
    public class QuestionScreenPresenter : IScreenPresenter
    {
        private readonly IQuestionScreenView _questionScreenView;
        private readonly IAnswerValidationSystem _answerValidationSystem; 
        private readonly IScreenSystem _screenSystem;
        private readonly IAnswerButtonPoolAdapter _answerButtonPoolAdapter;
        private readonly List<IAnswerButton> _answerButtons;
        public QuizScreen QuizScreen => QuizScreen.QuestionScreen;

        public QuestionScreenPresenter(IQuestionScreenView questionScreenView, IAnswerValidationSystem answerValidationSystem, IScreenSystem screenSystem, IAnswerButtonPoolAdapter answerButtonPoolAdapter)
        {
            _answerValidationSystem = answerValidationSystem;
            _screenSystem = screenSystem;
            _questionScreenView = questionScreenView;
            _answerButtonPoolAdapter = answerButtonPoolAdapter;
            _answerButtons = new List<IAnswerButton>(4);
        }

        public void Present()
        {
            if (_answerValidationSystem.CurrentQuestion != null)
            {
                _questionScreenView.QuestionImage.sprite = _answerValidationSystem.CurrentQuestion.QuestionInfo.QuestionSprite;
                _questionScreenView.QuestionText.text = _answerValidationSystem.CurrentQuestion.QuestionInfo.Question;
                foreach (var answers in _answerValidationSystem.CurrentQuestion.QuestionInfo.Answers)
                {
                    var answerButton = _answerButtonPoolAdapter.Spawn(_questionScreenView.AnswersContainer, answers);
                    answerButton.Clicked += OnAnswerButtonClicked;
                    _answerButtons.Add(answerButton);
                }
            }
            _questionScreenView.Show();
        }

        public void Hide()
        {
            _questionScreenView.Hide();
            foreach (var answerButton in _answerButtons)
            {
                _answerButtonPoolAdapter.Despawn(answerButton);
            }
            _answerButtons.Clear();
        }

        private async void OnAnswerButtonClicked(IAnswerButton answerButton)
        {
            var isCorrectAnswer = _answerValidationSystem.ValidateAnswer(answerButton.Answer);
            if (isCorrectAnswer) answerButton.MarkAsCorrectAnswer();
            else answerButton.MarkAsIncorrectAnswer();
            
            foreach (var button in _answerButtons)
                button.Clicked -= OnAnswerButtonClicked;
            
            await Task.Delay(TimeSpan.FromSeconds(2));
            
            _screenSystem.SwitchScreen(QuizScreen.QuestionsMap);
        }
    }
}