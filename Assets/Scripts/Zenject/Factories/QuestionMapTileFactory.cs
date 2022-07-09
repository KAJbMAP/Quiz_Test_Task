using Interfaces.Data;
using Interfaces.View;
using UnityEngine;
using View;

namespace Zenject.Factories
{
    public class QuestionMapTileFactory : PlaceholderFactory<RectTransform, Vector2, IQuestionAsset, IQuestionMapTile> { }

    public class CustomQuestionMapTileFactory : IFactory<RectTransform, Vector2, IQuestionAsset, IQuestionMapTile>
    {
        private const string PrefabPath = "Prefabs/UI/QuestionMapTile";
        
        public IQuestionMapTile Create(RectTransform parent, Vector2 anchoredPosition, IQuestionAsset questionAsset)
        {
            var mapTileQuestion = Resources.Load<QuestionMapTile>(PrefabPath);
            var mapTileObject = Object.Instantiate(mapTileQuestion, Vector3.zero, Quaternion.identity, parent);
            mapTileObject.transform.localScale = Vector3.one;
            mapTileObject.transform.localPosition = Vector3.zero;
            (mapTileObject.transform as RectTransform).anchorMin = Vector2.zero;
            (mapTileObject.transform as RectTransform).anchorMax = Vector2.zero;
            (mapTileObject.transform as RectTransform).anchoredPosition = anchoredPosition;
            mapTileObject.AssignQuestionAsset(questionAsset);
            return mapTileObject;
        }
    }
}