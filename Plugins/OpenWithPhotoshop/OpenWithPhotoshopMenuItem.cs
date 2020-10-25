using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Editor = UnityEditor.Editor;

public class OpenWithPhotoshopMenuItem : Editor {

    private static readonly string[] SupportedFileTypes = new[] {".png", ".jpg", ".gif", ".psd", ".bmp", ".psb"};
    private static string selectedAssetPath;

    /// <summary>
    /// Checks if file is valid.
    /// </summary>
    [MenuItem("Assets/Open with Photoshop", true)]
    private static bool OpenPhotoshopValidation() {

        //Is current selected asset a texture:
        if (Selection.activeObject is Texture) {

            //Get asset path:
            selectedAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());

            //Get asset file extension:
            var fileExtension = selectedAssetPath.Substring(selectedAssetPath.LastIndexOf(".", StringComparison.Ordinal));

            //Is file extension included in SupportedFileTypes:
            foreach (var fileType in SupportedFileTypes) {
                if (fileType.Equals(fileExtension)) {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// If OpenPhotoshopValidation() returns true, this method is called.
    /// Use % for ctrl (cmd on macOs), & for alt or # for shift. Use LEFT, RIGHT, UP, DOWN, F1 -> F12, HOME, END, PGUP,
    /// PGDN, SPACE for special keys. Read more <see href="ref https://docs.unity3d.com/2018.3/Documentation/ScriptReference/MenuItem.html">here</see>.
    /// </summary>
    [MenuItem("Assets/Open with Photoshop %SPACE")]
    private static void OpenPhotoshop() {
        
        //Get photoshop path:
        string photoshopPath = EditorPrefs.GetString(OpenWithPhotoshopSettings.Key, "");
        
        //Is path valid:
        if (!string.IsNullOrEmpty(photoshopPath)) {
            //Open photoshop with file:
            var texturePath = Application.dataPath + selectedAssetPath.Replace("Assets", "");
            Process.Start(photoshopPath, texturePath);
        } else {
            Debug.LogError("Please specify the path to Photoshop.exe under Edit/Preferences/Open with Photoshop");
        }
    }
}
