using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MobillersApp.UI
{
    [RequireComponent(typeof(Graphic))]
    public class FadeAnimation : UIAnimation
    {
        [Header("Fading Colors")]
        [SerializeField] Color fadeInColor = Color.white;
        [SerializeField] Color fadeOutColor = Color.clear;

        Graphic graphic;

        public override void SetUp()
        {
            graphic = GetComponent<Graphic>();
            Reset();
        }

        public override void Reset()
        {
            graphic.color = fadeOutColor;
        }

        public override Tween Show()
        {
            Tween tween = graphic.DOColor(fadeInColor, showDuration);
            tween.SetEase(showEase);

            return tween;
        }

        public override Tween Hide()
        {
            Tween tween = graphic.DOColor(fadeOutColor, hideDuration);
            tween.SetEase(hideEase);

            return tween;
        }
    }
}