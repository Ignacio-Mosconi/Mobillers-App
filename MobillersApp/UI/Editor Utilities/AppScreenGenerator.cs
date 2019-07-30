using System;
using UnityEngine;
using UnityEditor;

namespace MobillersApp.UI.EditorUtilities
{
    public class AppScreenGenerator : MonoBehaviour
    {
        const string WarningDialogueTitle = "Mobillers App - UI Tools Warning";

        const string CanvasMenuItemName = "MobillersApp/UI Tools/App Canvas/Create ";
        const string ScreensMenuItemName = "MobillersApp/UI Tools/App Screens/Create ";
        const string AppCanvasPath = "Assets/Prefabs/Mobillers App UI/App Canvas/";
        const string AppScreensPath = "Assets/Prefabs/Mobillers App UI/App Screens/";
        const string PrefabExtension = ".prefab";

        const string AppCanvasName = "App Canvas";
        const string LogInScreenName = "Log In Screen";
        const string SignUpScreenName = "Sign Up Screen";

        static void CreateCanvas()
        {
            try
            {
                GameObject canvas = AssetDatabase.LoadAssetAtPath<GameObject>(AppCanvasPath + AppCanvasName + PrefabExtension);

                if (!canvas)
                    throw new Exception("The '" + AppCanvasName + "' prefab could not be found. " +
                                        "Make sure it's located at the '" + AppCanvasPath + "' directory.");

                Instantiate(canvas).name = AppCanvasName;
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog(WarningDialogueTitle, e.Message, "Ok");
            }
        }

        static void CreateScreen(string prefabName)
        {
            try
            {
                GameObject screen = AssetDatabase.LoadAssetAtPath<GameObject>(AppScreensPath + prefabName + PrefabExtension);

                if (!screen)
                    throw new Exception("The '" + prefabName + "' prefab could not be found. " +
                                        "Make sure it's located at the '" + AppScreensPath + "' directory.");

                Canvas canvas = FindObjectOfType<Canvas>();

                if (!canvas)
                    throw new Exception("The scene doesn't have a canvas. You need to have one set up in order to " +
                                        "instantiate a new '" + prefabName + "'.");

                Instantiate(screen, canvas.transform).name = prefabName;
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog(WarningDialogueTitle, e.Message, "Ok");
            }
        }

        [MenuItem(CanvasMenuItemName + AppCanvasName)]
        public static void CreateAppCanvas()
        {
            CreateCanvas();
        }

        [MenuItem(ScreensMenuItemName + LogInScreenName)]
        public static void CreateLogInScreen()
        {
            CreateScreen(LogInScreenName);
        }

        [MenuItem(ScreensMenuItemName + SignUpScreenName)]
        public static void CreateSignUpScreen()
        {
            CreateScreen(SignUpScreenName);
        }
    }
}
