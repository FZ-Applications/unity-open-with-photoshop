using UnityEditor;
using UnityEngine;

public static class OpenWithPhotoshopSettings {

    public static string pathToPhotoshop;
    public const string Key = "OpenWithPhotoshop_PhotoshopPath";
    
    private class OpenWithPhotoshopSettingsProvider : SettingsProvider {
        public OpenWithPhotoshopSettingsProvider(string path, SettingsScope scopes = SettingsScope.User) : base(path, scopes) {
            pathToPhotoshop = EditorPrefs.GetString(Key, "");
        }

        public override void OnGUI(string searchContext) {
            OpenWithPhotoshopSettings.OnGUI();
        }
    }

    [SettingsProvider]
    private static SettingsProvider OpenWithPhotoshopPreferenceItem() {
        return new OpenWithPhotoshopSettingsProvider("Preferences/Open With Photoshop");
    }
    
    public static void OnGUI() {
        EditorGUILayout.LabelField("Please specify the path to Photoshop.exe:");
        
        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("Path to Photoshop.exe", GUILayout.Width(150));
        
        EditorGUILayout.SelectableLabel(pathToPhotoshop, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        
        //Choose button:
        if (GUILayout.Button("Choose")) {
            //Select photoshop.exe:
            pathToPhotoshop = EditorUtility.OpenFilePanel("Path to Photoshop.exe", "", "exe");
            EditorPrefs.SetString(Key, pathToPhotoshop);
        }
        
        EditorGUILayout.EndHorizontal();
    }
}