using UnityEngine;
using DG.Tweening;

namespace MobillersApp.UI
{
    public class AppNavigator : MonoBehaviour
    {
        [SerializeField] AppScreen homeScreen = default;
        [SerializeField] SlideAnimation backHeaderAnimation = default;

        AppMenu appMenu;
        AppScreen[] appScreens;
        AppScreen currentScreen;
        AppScreen previousScreen;
        
        void Awake()
        {
            appMenu = GetComponentInChildren<AppMenu>(includeInactive: true);
            appScreens = GetComponentsInChildren<AppScreen>(includeInactive: true);

            currentScreen = homeScreen;

            appMenu.SetUpAnimations();
            backHeaderAnimation.SetUp();

            foreach (AppScreen appScreen in appScreens)
            {
                appScreen.SetUpAnimations();
                if (appScreen != homeScreen)
                    appScreen.Deactivate();
            }
        }

        void Start()
        {
            homeScreen.Show();
            appMenu.Activate();
            backHeaderAnimation.Activate();
        }

        void CheckAppMenuAvailability(Sequence showSequence, Sequence hideSequence)
        {
            if (currentScreen == homeScreen)
            {
                appMenu.Activate(hideSequence);
                hideSequence.Insert(backHeaderAnimation.HideStartUpTime, backHeaderAnimation.Hide());
            }
            else
            {
                appMenu.Deactivate();
                showSequence.Insert(backHeaderAnimation.ShowStartUpTime, backHeaderAnimation.Show());
            }
        }

        public void MoveToScreen(AppScreen nextScreen)
        {
            Sequence hideSequence = currentScreen.Hide();
            previousScreen = currentScreen;

            Sequence showSequence = nextScreen.Show();
            currentScreen = nextScreen;

            CheckAppMenuAvailability(showSequence, hideSequence);
        }

        public void ReturnToPreviousScreen()
        {
            Sequence hideSequence = currentScreen.HideReversed();
            currentScreen = previousScreen;

            Sequence showSequence = previousScreen.Show();
            previousScreen = currentScreen.PreviousScreen;

            CheckAppMenuAvailability(showSequence, hideSequence);
        }
    }
}