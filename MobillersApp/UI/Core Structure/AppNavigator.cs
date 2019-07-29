using UnityEngine;
using UnityEngine.Events;

namespace MobillersApp.UI
{
    public class AppNavigator : MonoBehaviour
    {
        [SerializeField] AppScreen homeScreen = default;

        AppScreen[] appScreens;
        AppScreen currentScreen;
        AppScreen previousScreen;

        public UnityEvent onSwitchedScreen = new UnityEvent();
        
        void Awake()
        {
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
        }

        public void MoveToScreen(AppScreen nextScreen)
        {
            currentScreen.Hide();
            previousScreen = currentScreen;

            currentScreen = nextScreen;
            currentScreen.Show();

            onSwitchedScreen.Invoke();
        }

        public void ReturnToPreviousScreen()
        {
            currentScreen.HideReversed();
            currentScreen = previousScreen;

            previousScreen.Show();
            previousScreen = currentScreen.PreviousScreen;

            onSwitchedScreen.Invoke();
        }
    }
}