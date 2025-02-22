using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
public class SwitchSceneEditor
{
    private const string SPLASH_SCENE = "SplashScene";
    private const string MAIN_MENU_SCENE = "MainMenuScene";
    private const string GAMEPLAY_SCENE = "GameplayScene";

    [MenuItem("Scenes/Splash Scene")]
    public static void OpenSplashScene()
    {
        SceneHelper.StartScene(SPLASH_SCENE);
    }

    [MenuItem("Scenes/Main Menu Scene")]
    public static void OpenMainMenuScene()
    {
        SceneHelper.StartScene(MAIN_MENU_SCENE);
    }

    [MenuItem("Scenes/Gameplay Scene")]

    public static void OpenGameplayScene()
    {
        SceneHelper.StartScene(GAMEPLAY_SCENE);
    }
}

static class SceneHelper
{
    static string sceneToOpen;

    public static void StartScene(string sceneName)
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }

        sceneToOpen = sceneName;

        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        if (string.IsNullOrEmpty(sceneToOpen) ||
            EditorApplication.isPlaying || EditorApplication.isPaused ||
            EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorApplication.update -= OnUpdate;
            return;
        }

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            string[] guid = AssetDatabase.FindAssets(sceneToOpen + " t:Scene", null);
            if (guid.Length > 0)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guid[0]);
                EditorSceneManager.OpenScene(scenePath);
            }
            else
            {
                Debug.LogError("Scene not found: " + sceneToOpen);
            }
        }
        sceneToOpen = null;
    }
}
#endif