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
        [SerializeField] string unmatchPasswordsWarningTitle = default;
        [SerializeField, TextArea(3, 5)] string unmatchPasswordsWarningMsg = default;
        [SerializeField] string unmatchPasswordsWarningConfirm = default;

        bool EnteredMatchingPasswords()
        {
            return (passwordField.text == passwordConfirmationField.text);
        }

        void ResetPasswordFields()
        {
            passwordField.text = "";
            passwordConfirmationField.text = "";
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
                AppManager.Instance.DisplayWarning(unmatchPasswordsWarningTitle, unmatchPasswordsWarningMsg, unmatchPasswordsWarningConfirm,
                                                    ResetPasswordFields, native: false);
        }
    }
}