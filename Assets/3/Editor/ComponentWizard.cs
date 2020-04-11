using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace HelloWorld
{
    public class ComponentWizard : ScriptableWizard 
    {
        private const string CodeTemplate =
@"

";

        public string className;

        [MenuItem("Assets/3/ComponentWizard", priority = 10000)]
        public static void ShowExample()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (Directory.Exists(path) == false) return;
            ScriptableWizard.DisplayWizard<ComponentWizard>("ComponentWizard", "Create");
        }

        private void OnWizardCreate()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            Debug.Log(path);
        }
    }
}