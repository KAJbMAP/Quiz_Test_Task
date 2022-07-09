using Interfaces.Data;
using UnityEngine;
using View;

namespace Zenject.ObjectsPool
{
    public class AnswerButtonMemoryPool : MonoMemoryPool<RectTransform, IAnswer, AnswerButton>
    {
        protected override void Reinitialize(RectTransform parent, IAnswer answer, AnswerButton item)
        {
            item.Initialize(parent, answer);
        }

        protected override void OnDespawned(AnswerButton item)
        {
            base.OnDespawned(item); 
            item.OnDespawned();
        }
    }
}