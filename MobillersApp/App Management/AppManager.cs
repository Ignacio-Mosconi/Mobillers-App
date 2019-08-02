using UnityEngine;

namespace MobillersApp.AppManagement
{
    [RequireComponent(typeof(UserAuthenticator))]
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
                        instance.userAuthenticator = appManager.AddComponent<UserAuthenticator>();
                    }
                }

                return instance;
            }
        }

        #endregion

        UserAuthenticator userAuthenticator;

        void Awake()
        {
            SetUpSingleton();
            if (!userAuthenticator)
                userAuthenticator = GetComponent<UserAuthenticator>();
        }

        public void PerformSignUp(string email, string password)
        {
            userAuthenticator.SignUpUser(email, password);
        }

        public void PerformLogIn(string email, string password)
        {
            userAuthenticator.LogInUser(email, password);
        }
    }
}