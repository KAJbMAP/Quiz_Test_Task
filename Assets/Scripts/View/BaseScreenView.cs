using Interfaces.View;
using UnityEngine;

namespace View
{
    public class BaseScreenView : MonoBehaviour, IScreenView
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}