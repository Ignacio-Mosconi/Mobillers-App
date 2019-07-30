using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MobillersApp.UI
{
    [RequireComponent(typeof(Graphic))]
    public class FadeAnimation : UIAnimation
    {
        [SerializeField, Range(0f, 1f)] float targetShowAlpha = 1f; 
        [SerializeField, Range(0f, 1f)] float targetHideAlpha = 0f; 

        Graphic graphic;

        public override void SetUp()
        {
            graphic = GetComponent<Graphic>();
            Reset();
        }

        public override void Reset()
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, targetHideAlpha);
        }

        public override Tween Show()
        {
            Tween tween = graphic.DOFade(targetShowAlpha, showDuration);
            tween.SetEase(showEase);

            return tween;
        }

        public override Tween Hide()
        {
            Tween tween = graphic.DOFade(targetHideAlpha, hideDuration);
            tween.SetEase(hideEase);

            return tween;
        }
    }
}