using System;
using UnityEngine;
using UnityEngine.UI;
using IEnumerator = System.Collections.IEnumerator;

namespace UScreens
{
    public class UPanel : MonoBehaviour
    {
        public bool IsShowing => gameObject.activeSelf;

        [SerializeField] private Button hideBtn;

        [NonSerialized] protected IUPanelAnim panelAnim;

        [SerializeField,Min(0)] protected float currentHideDuration = .1f;

        private PanelEvent _Event;

        public virtual IUPanelAnim GetPanelAnim() => new AnimatorPanelAnim();

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            (panelAnim ??= GetPanelAnim())?.Validate(this);
#endif        
        }

        public virtual void Initialize()
        {
            if (hideBtn)
                hideBtn.onClick.AddListener(HideClicked);
            (panelAnim = GetPanelAnim())?.Initialize(this);
            _Event = GetComponent<PanelEvent>();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
            panelAnim?.Show();
            _Event?.DoShow();
        }

        public virtual void Hide()
        {
            if (!IsShowing)
                return;

            _Event?.DoBeforeHide();

            if (panelAnim != null)
            {
                var animationDuration = panelAnim.Hide();
                if (currentHideDuration < animationDuration) 
                    currentHideDuration = animationDuration;
                
                Invoke(nameof(HideForce), currentHideDuration);
            }
            else
                HideForce();
        }

        public virtual void HideForce()
        {
            _Event?.DoHide();
            gameObject.SetActive(false);
        }

        protected virtual void HideClicked() =>
            Hide();
    }
}