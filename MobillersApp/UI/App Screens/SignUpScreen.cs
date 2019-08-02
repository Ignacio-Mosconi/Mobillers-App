using UnityEngine;
using TMPro;
using MobillersApp.AppManagement;

namespace MobillersApp.UI
{
    public class SignUpScreen : AppScreen
    {
        [SerializeField] TMP_InputField emailField = default;    
        [SerializeField] TMP_InputField passwordField = default;
        [SerializeField] TMP_InputField passwordConfirmationField = default;

        bool EnteredMatchingPasswords()
        {
            return (passwordField.text == passwordConfirmationField.text);
        }

        public void SignUpUser()
        {
            if (EnteredMatchingPasswords())
            {
                string email = emailField.text;
                string password = passwordField.text;

                AppManager.Instance.PerformSignUp(email, password);
            }
            else
            {
                passwordField.text = "";
                passwordConfirmationField.text = "";
            }
        }
    }
}