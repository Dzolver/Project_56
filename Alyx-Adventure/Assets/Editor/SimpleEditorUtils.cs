using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class SimpleEditorUtils
{
    // click Ctrl+Q to go to the prelaunch scene and then play
    //% - ctrl on Windows, cmd on macOS
    //# - shift
    //& - alt
    [MenuItem("Edit/Play From Any Scene-Unplay  %Q")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Splash.unity");
        EditorApplication.isPlaying = true;
    }
}