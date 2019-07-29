using UnityEngine;
using DG.Tweening;

namespace MobillersApp.UI
{
    public class AppScreen : MonoBehaviour
    {
        [SerializeField] AppScreen previousScreen = default;

        UIAnimation[] uiAnimations;
        Sequence animationSequence;

        public virtual void Show()
        {
            animationSequence = DOTween.Sequence();
            Activate();
            foreach (UIAnimation uiAnimation in uiAnimations)
                animationSequence.Insert(uiAnimation.ShowStartUpTime, uiAnimation.Show());
        }

        public virtual void Hide()
        {
            animationSequence = DOTween.Sequence();
            foreach (UIAnimation uiAnimation in uiAnimations)
                animationSequence.Insert(uiAnimation.HideStartUpTime, uiAnimation.Hide());
            animationSequence.OnComplete(Deactivate);
        }

        public virtual void HideReversed()
        {
            animationSequence = DOTween.Sequence();
            foreach (UIAnimation uiAnimation in uiAnimations)
            {
                SlideAnimation slideAnimation = uiAnimation as SlideAnimation;
                if (slideAnimation)
                    animationSequence.Insert(slideAnimation.HideStartUpTime, slideAnimation.HideReversed());
                else
                    animationSequence.Insert(uiAnimation.HideStartUpTime, uiAnimation.Hide());
            }
            animationSequence.OnComplete(Deactivate);
        }

        public void SetUpAnimations()
        {
            uiAnimations = GetComponentsInChildren<UIAnimation>(includeInactive: true);
            foreach (UIAnimation uiAnimation in uiAnimations)
                uiAnimation.SetUp();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        #region Properties

        public AppScreen PreviousScreen
        {
            get { return previousScreen; }
        }

        #endregion
    }
}