using UnityEngine;
using UnityEngine.Events;

namespace UScreens
{
    [RequireComponent(typeof(UPanel))]
    internal class PanelEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnShow;
        [SerializeField] private UnityEvent<float> OnHide;

        public void DoShow() => OnShow?.Invoke();
        public void DoHide(float duration) => OnHide?.Invoke(duration);
    }
}