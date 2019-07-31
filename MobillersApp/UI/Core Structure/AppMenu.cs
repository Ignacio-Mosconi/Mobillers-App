using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MobillersApp.UI
{
    public class AppMenu : MonoBehaviour
    {
        [Header("Main Buttons")]
        [SerializeField] Button menuButton = default;
        [SerializeField] Button closeButton = default;

        [Header("Animations")]
        [SerializeField] SlideAnimation slidingMenuAnimation = default;
        [SerializeField] SlideAnimation menuButtonAnimation = default;
        [SerializeField] FadeAnimation backgroundBlockerAnimation = default;

        [Header("Finger Slide Properties")]
        [SerializeField, Range(0.1f, 0.2f)] float viewportDragPositionShow = 0.1f;
        [SerializeField, Range(0f, 1f)] float showDeltaPercentage = 0.5f;
        [SerializeField, Range(0f, 1f)] float hideDeltaPercentage = 0.3f;

        RectTransform slidingMenuRectTransform;
        Vector2 dragStartPosition;
        float dragAcummulator;
        float menuActivationDelta;
        float menuDeactivationDelta;
        float showHorDragPosition;
        float hideHorDragPosition;
        bool beganValidDrag;
        bool isSlidePlaying;
        bool isMenuActive;

        const float NegligibleDeltaTouchPosition = 5f;

        void Awake()
        {
            slidingMenuRectTransform = slidingMenuAnimation.gameObject.GetComponent<RectTransform>();
        }

        void Start()
        {
            showHorDragPosition = Camera.main.ViewportToScreenPoint(new Vector3(viewportDragPositionShow, 0f, 0f)).x;
            hideHorDragPosition = slidingMenuRectTransform.rect.size.x;
            menuActivationDelta = slidingMenuRectTransform.rect.size.x * showDeltaPercentage;
            menuDeactivationDelta = -slidingMenuRectTransform.rect.size.x * hideDeltaPercentage;

            menuButton.onClick.AddListener(ShowMenu);
            closeButton.onClick.AddListener(HideMenu);
            
            backgroundBlockerAnimation.Deactivate();
        }

        void Update()
        {
            if (Input.touchCount != 1 || isSlidePlaying)
                return;

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Moved:

                    if (!beganValidDrag)
                        CheckValidDragStart(touch);
                    else
                        UpdateSlidingMenuPosition(touch);
                    break;

                case TouchPhase.Ended:
                    
                    if (beganValidDrag)
                        CheckMenuActivation(touch);
                    break;
            }
        }

        void CheckValidDragStart(Touch touch)
        {
            if (Mathf.Abs(touch.deltaPosition.x) > Mathf.Abs(touch.deltaPosition.y))
            {
                if (!isMenuActive)
                {
                    if (touch.position.x <= showHorDragPosition)
                    {
                        beganValidDrag = true;
                        dragStartPosition = touch.position;
                    }
                }
                else
                {
                    if (touch.position.x <= hideHorDragPosition)
                    {
                        beganValidDrag = true;
                        dragStartPosition = touch.position;
                    }
                }
            }
        }

        void UpdateSlidingMenuPosition(Touch touch)
        {
            dragAcummulator += touch.deltaPosition.x;

            if (Mathf.Abs(dragAcummulator) >= NegligibleDeltaTouchPosition)
            {
                float newHorPos = slidingMenuRectTransform.anchoredPosition.x + dragAcummulator;

                dragAcummulator = 0f;
                newHorPos = Mathf.Clamp(newHorPos, slidingMenuAnimation.SlideInOffscreenPosition.x, slidingMenuAnimation.InitialPosition.x);
                slidingMenuRectTransform.anchoredPosition = new Vector2(newHorPos, slidingMenuRectTransform.anchoredPosition.y);
            }
        }

        void CheckMenuActivation(Touch touch)
        {
            float horTouchDelta = touch.position.x - dragStartPosition.x;
            
            beganValidDrag = false;
            dragStartPosition = touch.position;

            bool shouldShowMenu = (horTouchDelta > menuActivationDelta && !isMenuActive);
            bool shouldHideMenu = (horTouchDelta < menuDeactivationDelta && isMenuActive);

            if (shouldShowMenu || shouldHideMenu)
            {
                if (shouldShowMenu)
                    ShowMenu();
                else
                    HideMenu();
            }
            else
            {
                if (!isMenuActive)
                    slidingMenuAnimation.Hide();
                else
                    slidingMenuAnimation.Show();
            }
        }

        void ToggleMenuStatus()
        {
            isSlidePlaying = false;
            isMenuActive = !isMenuActive;
        }

        void ShowMenu()
        {
            isSlidePlaying = true;
            backgroundBlockerAnimation.Activate();
            backgroundBlockerAnimation.Show();
            Tween slideTween = slidingMenuAnimation.Show();
            slideTween.OnComplete(ToggleMenuStatus);
        }

        void HideMenu()
        {
            isSlidePlaying = true;
            Tween fadeTween = backgroundBlockerAnimation.Hide();
            Tween slideTween = slidingMenuAnimation.Hide();
            fadeTween.OnComplete(backgroundBlockerAnimation.Deactivate);
            slideTween.OnComplete(ToggleMenuStatus);
        }

        public void SetUpAnimations()
        {
            slidingMenuAnimation.SetUp();
            menuButtonAnimation.SetUp();
            backgroundBlockerAnimation.SetUp();
        }

        public void Activate(Sequence sequence = null)
        {
            gameObject.SetActive(true);
            Tween menuButtonTween = menuButtonAnimation.Show();
            if (sequence != null)
                sequence.Append(menuButtonTween);
        }

        public void Deactivate(Sequence sequence = null)
        {
            Tween menuButtonTween = menuButtonAnimation.Hide();
            if (sequence != null)
            {
                sequence.Append(menuButtonTween);
                sequence.OnComplete(() => gameObject.SetActive(false));
            }
            else
                gameObject.SetActive(false);
        }
    }
}