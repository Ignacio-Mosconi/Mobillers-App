using UnityEngine;
using DG.Tweening;

namespace MobillersApp.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SlideAnimation : UIAnimation
    {
        [Header("Offscreen Positions")]
        [SerializeField] Vector2 slideInOffscreenPosition = default;
        [SerializeField] Vector2 slideOutOffscreenPosition = default;

        RectTransform rectTransform;
        Vector2 initialPosition;

        public override void SetUp()
        {
            rectTransform = GetComponent<RectTransform>();
            initialPosition = rectTransform.anchoredPosition;
            Reset();
        }

        public override void Reset()
        {
            rectTransform.anchoredPosition = slideInOffscreenPosition;
        }

        public override Tween Show()
        {
            Tween tween = rectTransform.DOAnchorPos(initialPosition, showDuration);
            tween.SetEase(showEase);

            return tween;
        }

        public override Tween Hide()
        {
            Tween tween = rectTransform.DOAnchorPos(slideOutOffscreenPosition, hideDuration);
            tween.SetEase(hideEase);

            return tween;
        }

        public Tween HideReversed()
        {
            Tween tween = rectTransform.DOAnchorPos(slideInOffscreenPosition, hideDuration);
            tween.SetEase(hideEase);

            return tween;
        }

        #region Properties

        public Vector2 SlideInOffscreenPosition
        {
            get { return slideInOffscreenPosition; }
        }

        public Vector2 SlideOutOffscreenPosition
        {
            get { return slideOutOffscreenPosition; }
        }

        public Vector2 InitialPosition
        {
            get { return initialPosition; }
        }

        #endregion
    }
}