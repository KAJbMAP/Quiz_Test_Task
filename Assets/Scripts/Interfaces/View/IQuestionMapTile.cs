using System;
using Interfaces.Data;
using UnityEngine;

namespace Interfaces.View
{
    public interface IQuestionMapTile
    {
        Vector2 AnchoredPosition { get; }
        IQuestionAsset QuestionAsset { get; }
        void MarkAsAnsweredCorrect();
        void MarkAsAnsweredIncorrect();
        void MarkAsActive();
        void MarkAsInactive();
        void SetNeighboursLines(ITileChildsInfo tileChildsInfo);
        event Action<IQuestionMapTile> Clicked;
    }
}