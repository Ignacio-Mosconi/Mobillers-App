using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace MobillersApp.UI
{
    public class AppNavigator : MonoBehaviour
    {
        [SerializeField] AppScreen homeScreen = default;

        AppMenu appMenu;
        AppScreen[] appScreens;
        AppScreen currentScreen;
        AppScreen previousScreen;

        public UnityEvent onSwitchedScreen = new UnityEvent();
        
        void Awake()
        {
            appMenu = GetComponentInChildren<AppMenu>(includeInactive: true);
            appScreens = GetComponentsInChildren<AppScreen>(includeInactive: true);

            currentScreen = homeScreen;

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
        }

        void CheckAppMenuAvailability(Sequence sequence = null)
        {
            if (currentScreen == homeScreen)
                appMenu.Activate(sequence);
            else
                appMenu.Deactivate(sequence);
        }

        public void MoveToScreen(AppScreen nextScreen)
        {
            currentScreen.Hide();
            previousScreen = currentScreen;

            nextScreen.Show();
            currentScreen = nextScreen;

            CheckAppMenuAvailability();

            onSwitchedScreen.Invoke();
        }

        public void ReturnToPreviousScreen()
        {
            Sequence hideSequence = currentScreen.HideReversed();
            currentScreen = previousScreen;

            previousScreen.Show();
            previousScreen = currentScreen.PreviousScreen;

            CheckAppMenuAvailability(hideSequence);

            onSwitchedScreen.Invoke();
        }
    }
}