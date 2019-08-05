using System;
using UnityEngine;

namespace MobillersApp.AppManagement
{
    [RequireComponent(typeof(UserAuthenticator))]
    [RequireComponent(typeof(AppAlertDispatcher))]
    public class AppManager : MonoBehaviour
    {
        #region Singleton

        static AppManager instance;

        void SetUpSingleton()
        {
            if (Instance == this)
                DontDestroyOnLoad(gameObject);
            else
                Destroy(gameObject);
        }

        public static AppManager Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<AppManager>();
                    if (!instance)
                    {
                        GameObject appManager = new GameObject("App Manager");
                        instance = appManager.AddComponent<AppManager>();
                        appManager.AddComponent<UserAuthenticator>();
                        appManager.AddComponent<AppAlertDispatcher>();
                    }
                }

                return instance;
            }
        }

        #endregion

        UserAuthenticator userAuthenticator;
        AppAlertDispatcher appAlertDispatcher;

        void Awake()
        {
            SetUpSingleton();
            userAuthenticator = GetComponent<UserAuthenticator>();
            appAlertDispatcher = GetComponent<AppAlertDispatcher>();
        }

        public void PerformSignUp(string email, string password)
        {
            userAuthenticator.SignUpUser(email, password);
        }

        public void PerformLogIn(string email, string password)
        {
            userAuthenticator.LogInUser(email, password);
        }

        public void DisplayWarning(string title, string message, string positiveText, Action positiveAction = null, bool native = true)
        {
            if (native)
                appAlertDispatcher.ShowNativeWarningAlert(title, message, positiveText, positiveAction);
            else
                appAlertDispatcher.ShowCustomWarningAlert(title, message, positiveText, positiveAction);
        }

        public void DisplayConfirmation(string title, string message, string positiveText, string negativeText, 
                                        Action positiveAction = null, Action negativeAction = null, bool native = true)
        {
            if (native)
                appAlertDispatcher.ShowNativeConfirmationAlert(title, message, positiveText, negativeText, positiveAction, negativeAction);
            else
                appAlertDispatcher.ShowCustomConfirmationAlert(title, message, positiveText, negativeText, positiveAction, negativeAction);
        }
    }
}