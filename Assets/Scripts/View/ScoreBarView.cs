using DG.Tweening;
using Interfaces.View;
using TMPro;
using UnityEngine;

namespace View
{
    public class ScoreBarView : MonoBehaviour, IScoreBarView
    {
        private const float AnimationDuration = 1.5f; 
        [SerializeField] private RectTransform _scoreIconTransform;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private int _currentScore;

        public void UpdateScore(int newScore)
        {
            var scoreDelta = newScore - _currentScore;
            var sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(() => _currentScore, score => _currentScore = score, newScore, AnimationDuration).OnUpdate(() => _scoreText.text = _currentScore.ToString()));
            sequence.Join(_scoreIconTransform.DOScale(Vector3.one * 1.1f, AnimationDuration / scoreDelta).SetEase(Ease.Flash).SetLoops(scoreDelta));
            sequence.OnComplete(() => _scoreIconTransform.localScale = Vector3.one);
        }
    }
}