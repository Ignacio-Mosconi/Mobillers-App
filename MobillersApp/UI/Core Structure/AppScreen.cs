using UnityEngine;
using DG.Tweening;

namespace MobillersApp.UI
{
    public class AppScreen : MonoBehaviour
    {
        [SerializeField] AppScreen previousScreen = default;

        UIAnimation[] uiAnimations;

        public virtual Sequence Show()
        {
            Sequence animationSequence = DOTween.Sequence();
            Activate();
            foreach (UIAnimation uiAnimation in uiAnimations)
                animationSequence.Insert(uiAnimation.ShowStartUpTime, uiAnimation.Show());

            return animationSequence;
        }

        public virtual Sequence Hide()
        {
            Sequence animationSequence = DOTween.Sequence();
            foreach (UIAnimation uiAnimation in uiAnimations)
                animationSequence.Insert(uiAnimation.HideStartUpTime, uiAnimation.Hide());
            animationSequence.OnComplete(Deactivate);

            return animationSequence;
        }

        public virtual Sequence HideReversed()
        {
            Sequence animationSequence = DOTween.Sequence();
            foreach (UIAnimation uiAnimation in uiAnimations)
            {
                SlideAnimation slideAnimation = uiAnimation as SlideAnimation;
                if (slideAnimation)
                    animationSequence.Insert(slideAnimation.HideStartUpTime, slideAnimation.HideReversed());
                else
                    animationSequence.Insert(uiAnimation.HideStartUpTime, uiAnimation.Hide());
            }
            animationSequence.OnComplete(Deactivate);

            return animationSequence;
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