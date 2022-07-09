using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Interfaces.Data;
using Interfaces.Model.Systems;
using Interfaces.Presenters;
using Interfaces.Providers;
using Interfaces.View;
using UnityEngine;
using Zenject.Factories;

namespace Presenters
{
    public class QuestionsMapScreenPresenter : IScreenPresenter, IDisposable
    {
        private const float TileSpacing = 170f;
        private readonly Vector2 StartTilePosition = new Vector2(200f, 200f); 
        
        private readonly IQuestionsMapScreenView _questionsMapScreenView;
        private readonly IQuestionsAssetsProvider _questionsAssetsProvider;
        private readonly IAnswerValidationSystem _answerValidationSystem;
        private readonly IScreenSystem _screenSystem;
        private readonly QuestionMapTileFactory _questionMapTileFactory;
        private readonly MapTileChildsInfoFactory _mapTileChildsInfoFactory;
        private readonly IReadOnlyDictionary<TileChilds, Vector2> _tileChildsOffset;

        private Dictionary<string, IQuestionMapTile> _questionMapTiles; 

        public QuizScreen QuizScreen => QuizScreen.QuestionsMap;

        public QuestionsMapScreenPresenter(IQuestionsMapScreenView questionsMapScreenView, IQuestionsAssetsProvider questionsAssetsProvider, QuestionMapTileFactory questionMapTileFactory,
            MapTileChildsInfoFactory mapTileChildsInfoFactory, IAnswerValidationSystem answerValidationSystem, IScreenSystem screenSystem)
        {
            _questionsMapScreenView = questionsMapScreenView;
            _questionsAssetsProvider = questionsAssetsProvider;
            _answerValidationSystem = answerValidationSystem;
            _screenSystem = screenSystem;
            _questionMapTileFactory = questionMapTileFactory;
            _mapTileChildsInfoFactory = mapTileChildsInfoFactory;
            _questionMapTiles = new Dictionary<string, IQuestionMapTile>();
            _tileChildsOffset = new Dictionary<TileChilds, Vector2>
            {
                {TileChilds.Left, Vector2.left * TileSpacing},   
                {TileChilds.Top, Vector2.up * TileSpacing},  
                {TileChilds.Right, Vector2.right * TileSpacing}, 
                {TileChilds.Bottom, Vector2.down * TileSpacing}   
            };
            
            _answerValidationSystem.QuestionAnswered += OnAnswerValidationSystemQuestionAnswered;
            
            SpawnQuestionTiles();            
        }

        public void Dispose()
        {
            foreach (var questionMapTile in _questionMapTiles.Values)
            {
                questionMapTile.Clicked -= OnQuestionMapTileClicked;
            }
            _answerValidationSystem.QuestionAnswered -= OnAnswerValidationSystemQuestionAnswered;
        }

        public void Present()
        {
            _questionsMapScreenView.Show();
        }

        public void Hide()
        {
            _questionsMapScreenView.Hide();
        }

        private void SpawnQuestionTiles()
        {
            //Enqueue a root question asset
            var questionMapTilesQueue = new Queue<IQuestionAsset>(16);
            questionMapTilesQueue.Enqueue(_questionsAssetsProvider.RootQuestion);
            
            //Instantiate root question tile
            var rootQuestionMapTile = _questionMapTileFactory.Create(_questionsMapScreenView.TileContainer, StartTilePosition, _questionsAssetsProvider.RootQuestion);
            var neighboursInfo = _mapTileChildsInfoFactory.Create(TileSpacing, _questionsAssetsProvider.RootQuestion.TileChilds);
            rootQuestionMapTile.SetNeighboursLines(neighboursInfo);
            _questionMapTiles.Add(rootQuestionMapTile.QuestionAsset.QuestionId, rootQuestionMapTile);


            //Main cycle
            while (questionMapTilesQueue.Count > 0)
            {
                //Get tile position as origin to spawn childs tiles
                var tileOriginPosition = _questionMapTiles[questionMapTilesQueue.Peek().QuestionId].AnchoredPosition;
                
                //Loop through all childs of current tile
                foreach (var questionChildInfo in questionMapTilesQueue.Peek().QuestionChilds)
                {
                    //Spawn question tile
                    var tilePosition = tileOriginPosition + _tileChildsOffset[questionChildInfo.tileChildDirection];
                    var questionMapTile = _questionMapTileFactory.Create(_questionsMapScreenView.TileContainer, tilePosition, questionChildInfo.questionAsset);
                    neighboursInfo = _mapTileChildsInfoFactory.Create(TileSpacing, questionChildInfo.questionAsset.TileChilds);
                    questionMapTile.SetNeighboursLines(neighboursInfo);
                    
                    //Add a new tile to the dictionary of all tiles
                    _questionMapTiles.Add(questionMapTile.QuestionAsset.QuestionId, questionMapTile);

                    
                    //Add question asset to queue
                    questionMapTilesQueue.Enqueue(questionChildInfo.questionAsset);
                }
                //Remove current question asset
                questionMapTilesQueue.Dequeue();
            }

            foreach (var questionMapTile in _questionMapTiles.Values)
            {
                questionMapTile.MarkAsInactive();
                questionMapTile.Clicked += OnQuestionMapTileClicked;
            }
            rootQuestionMapTile.MarkAsActive();
        }

        private void OnQuestionMapTileClicked(IQuestionMapTile questionMapTile)
        {
            _answerValidationSystem.PickQuestion(questionMapTile.QuestionAsset);
            _screenSystem.SwitchScreen(QuizScreen.QuestionScreen);
        }

        private void OnAnswerValidationSystemQuestionAnswered(IQuestionAsset questionAsset, bool isCorrectAnswer)
        {
            if (isCorrectAnswer) _questionMapTiles[questionAsset.QuestionId].MarkAsAnsweredCorrect();
            else _questionMapTiles[questionAsset.QuestionId].MarkAsAnsweredIncorrect();
            
            foreach (var questionChild in questionAsset.QuestionChilds)
            {
                _questionMapTiles[questionChild.questionAsset.QuestionId].MarkAsActive();
            }
        }
    }
}