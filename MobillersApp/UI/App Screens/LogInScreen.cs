using UnityEngine;
using TMPro;
using MobillersApp.AppManagement;

namespace MobillersApp.UI
{
    public class LogInScreen : AppScreen
    {
        [SerializeField] TMP_InputField emailField = default;    
        [SerializeField] TMP_InputField passwordField = default;

        public void LogInUser()
        {
            string email = emailField.text;
            string password = passwordField.text;

            AppManager.Instance.PerformLogIn(email, password);
        }
    }
}