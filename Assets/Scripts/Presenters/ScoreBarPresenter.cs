using System;
using Interfaces.Model.Systems;
using Interfaces.Presenters;
using Interfaces.View;

namespace Presenters
{
    public class ScoreBarPresenter : IDisposable, IScoreBarPresenter
    {
        private readonly IScoreSystem _scoreSystem;
        private readonly IScoreBarView _scoreBarView;
        
        public ScoreBarPresenter(IScoreSystem scoreSystem, IScoreBarView scoreBarView)
        {
            _scoreSystem = scoreSystem;
            _scoreBarView = scoreBarView;
            
            _scoreSystem.ScoreUpdated += OnScoreSystemScoreUpdated;
        }

        public void Dispose()
        {
            _scoreSystem.ScoreUpdated -= OnScoreSystemScoreUpdated;
        }
        
        private void OnScoreSystemScoreUpdated(int newScore)
        {
            _scoreBarView.UpdateScore(newScore);
        }
    }
}