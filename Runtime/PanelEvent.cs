using UnityEngine;
using UnityEngine.Events;

namespace UScreens
{
    [RequireComponent(typeof(UPanel))]
    internal class PanelEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnShow;
        [SerializeField] private UnityEvent OnBeforeHide;
        [SerializeField] private UnityEvent OnHide;

        public void DoShow() => OnShow?.Invoke();
        public void DoBeforeHide() => OnBeforeHide?.Invoke();
        public void DoHide() => OnHide?.Invoke();
    }
}