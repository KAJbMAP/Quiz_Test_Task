using Interfaces.Data;
using Interfaces.View;
using UnityEngine;
using View;

namespace Zenject.ObjectsPool
{
    public interface IAnswerButtonPoolAdapter
    {
        IAnswerButton Spawn(RectTransform parent, IAnswer answer);
        void Despawn(IAnswerButton despawnedObject);
    }

    public class AnswerButtonPoolAdapter : IAnswerButtonPoolAdapter
    {
        private readonly AnswerButtonMemoryPool _answerButtonMemoryPool;
        
        public AnswerButtonPoolAdapter(AnswerButtonMemoryPool answerButtonMemoryPool)
        {
            _answerButtonMemoryPool = answerButtonMemoryPool;
        }
        
        public IAnswerButton Spawn(RectTransform parent, IAnswer answer)
        {
            return _answerButtonMemoryPool.Spawn(parent, answer);
        }

        public void Despawn(IAnswerButton despawnedObject)
        {
            _answerButtonMemoryPool.Despawn(despawnedObject as AnswerButton);
        }
    }
}