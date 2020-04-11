using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class OverrideMonoBehaviour : UnityEditor.AssetModificationProcessor
{
    private const string CodeTemplate =
@"using UnityEngine;
using System.Collections.Generic;

namespace 命名空间
{
    public class 类名 : MonoBehaviour 
    {

    }
}";

    private static void OnWillCreateAsset(string name)
    {
        if (EditorPrefs.GetBool("OverrideMonoBehaviour_IsUse", false) == false) return;
        string overrideNamespace = EditorPrefs.GetString("OverrideMonoBehaviour_Namespace", null);
        if (overrideNamespace == null) return;

        // 去掉前面的Assets和后面的.meta
        if (name.Length < 11) return;
        name = name.Substring(6);
        name = name.Substring(0, name.Length - 5);

        // 去掉不是脚本的东西
        if (name.EndsWith(".cs") == false) return;

        // 读取脚本
        string path = Application.dataPath + name;
        string text = File.ReadAllText(path);

        // 去掉不是Unity模板
        if (text.Contains("// Start is called before the first frame update") == false) return;
        if (text.Contains("// Update is called once per frame") == false) return;

        string className = GetClassName(text);
        if (className == null)
        {
            Debug.LogWarning("没有获取到类名");
            return;
        }

        string code = CodeTemplate.Replace("命名空间", overrideNamespace);
        code = code.Replace("类名", className);
        File.WriteAllText(path, code);
    }

    private static string GetClassName(string text)
    {
        string pattern = "public class ([A-Za-z0-9_]+) : MonoBehaviour";
        Match match = Regex.Match(text, pattern);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        else
        {
            return null;
        }
    }
}
