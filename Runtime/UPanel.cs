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

        [NonSerialized] protected float currentHideDuration = .1f;

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

            if (panelAnim != null)
            {
                currentHideDuration = panelAnim.Hide();
                StartCoroutine(HideAnimation());
            }
            else
                HideForce();

            _Event?.DoHide(currentHideDuration);
        }

        private IEnumerator HideAnimation()
        {
            yield return new WaitForSecondsRealtime(currentHideDuration);
            HideForce();
        }

        public virtual void HideForce() =>
            gameObject.SetActive(false);

        protected virtual void HideClicked() =>
            Hide();
    }
}