using System;

namespace Interfaces.Model.Systems
{
    public interface IScoreSystem
    {
        int CurrentScore { get; }
        event Action<int> ScoreUpdated;
    }
}