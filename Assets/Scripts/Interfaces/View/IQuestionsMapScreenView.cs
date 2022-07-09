using UnityEngine;

namespace Interfaces.View
{
    public interface IQuestionsMapScreenView : IScreenView
    {
        RectTransform TileContainer { get; }
    }
}