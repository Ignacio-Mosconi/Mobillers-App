using System.Threading.Tasks;
using UnityEngine;
using Firebase.Auth;

namespace MobillersApp.AppManagement
{
    public class UserAuthenticator : MonoBehaviour
    {
        FirebaseAuth authenticator;

        void Awake()
        {
            authenticator = FirebaseAuth.DefaultInstance;
        }

        public void SignUpUser(string email, string password)
        {
            Task<FirebaseUser> signUpTask = authenticator.CreateUserWithEmailAndPasswordAsync(email, password);

            signUpTask.ContinueWith(task => 
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
        }

        public void LogInUser(string email, string password)
        {
            Task<FirebaseUser> signUpTask = authenticator.SignInWithEmailAndPasswordAsync(email, password);

            signUpTask.ContinueWith(task => 
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user logged in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            });
        }
    }
}