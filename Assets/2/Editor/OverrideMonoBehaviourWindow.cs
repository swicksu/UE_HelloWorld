using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.UI;
using Toggle = UnityEngine.UIElements.Toggle;

public class OverrideMonoBehaviourWindow : EditorWindow
{
    private VisualElement root;

    [MenuItem("´°¿Ú/2/OverrideMonoBehaviourWindow")]
    public static void ShowExample()
    {
        OverrideMonoBehaviourWindow wnd = GetWindow<OverrideMonoBehaviourWindow>();
        wnd.titleContent = new GUIContent("OverrideMonoBehaviourWindow");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/2/Editor/OverrideMonoBehaviourWindow.uxml");
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/2/Editor/OverrideMonoBehaviourWindow.uss");
        VisualElement labelFromUXML = visualTree.CloneTree();
        labelFromUXML.styleSheets.Add(styleSheet);
        root.Add(labelFromUXML);

        TextField namespaceField = root.Q<TextField>("Namespace");
        Toggle isUseToggle = root.Q<Toggle>("IsUse");
        namespaceField.value = EditorPrefs.GetString("OverrideMonoBehaviour_Namespace", "HelloWorld");
        isUseToggle.value = EditorPrefs.GetBool("OverrideMonoBehaviour_IsUse", true);

        namespaceField.RegisterCallback<ChangeEvent<string>>(ChangeNamespace);
        isUseToggle.RegisterCallback<ChangeEvent<bool>>(ChangeIsUse);
    }

    private void ChangeNamespace(ChangeEvent<string> evt)
    {
        TextField namespaceField = root.Q<TextField>("Namespace");
        namespaceField.value = evt.newValue;
        EditorPrefs.SetString("OverrideMonoBehaviour_Namespace", evt.newValue);
    }

    private void ChangeIsUse(ChangeEvent<bool> evt)
    {
        Toggle isUseToggle = root.Q<Toggle>("IsUse");
        isUseToggle.value = evt.newValue;
        EditorPrefs.SetBool("OverrideMonoBehaviour_IsUse", evt.newValue);
    }
}