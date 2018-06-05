
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
//%代表ctrl，#代表Shift，&代表Alt
public class ResourceEdit
{
    public static List<string> lstPrefabPath = new List<string>()
    {
        Application.dataPath + "/UI/Hall/Panel",
        Application.dataPath + "/UI/Hall/Grid",
        Application.dataPath + "/UI/Mahjong/Panel",
        Application.dataPath + "/UI/Mahjong/Grid",
    };

    static public void SetParentLayer(Transform child)
    {
        if (child.parent == null)
            return;

        if (child.parent.gameObject.layer != 5)
            child.parent.gameObject.layer = 5;

        SetParentLayer(child.parent);
    }

   // [MenuItem("Plugin/断开字体关联(只供UI制作人员使用，请勿随意操作)", false, 9)]
    public static void CorrectionPublicDisconnectFontFunction ( )
    {
//         if (NGUISettings.ambigiousFont == null)
//         {
//             Debug.LogError("对不起！你没有指定字体！");
//         }
//         else
//         {
//             CorrectionPublicDisconnectFont();
//         }
    }


    [MenuItem("Plugin/使用选中字体替换所有prefab字体", false, 2)]
    public static void CorrectionPublicFontAllFunction ( )
    {
        Font font = Selection.activeObject as Font;
        CorrectionPublicFont(font, null);
    }


    private static void SaveDealFinishPrefab ( GameObject go, string path )
    {
        if (File.Exists(path) == true)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
            PrefabUtility.ReplacePrefab(go, prefab);
        }
        else
        {
            PrefabUtility.CreatePrefab(path, go);
        }
    }

    private static void ChangeFontByPath(string dir, Font replace, Font matching)
    {
        DirectoryInfo dicInfo = new DirectoryInfo(dir);
        FileInfo[] fileInfos = dicInfo.GetFiles("*.prefab", SearchOption.AllDirectories);
        foreach (FileInfo file in fileInfos)
        {
            string fullName = file.FullName;
            Debug.Log("path = " + fullName);


            string assePath = fullName.Substring(fullName.IndexOf("Assets"));
            GameObject selectObj = AssetDatabase.LoadAssetAtPath(assePath, typeof(GameObject)) as GameObject;
            Debug.Log("prefab = " + selectObj.name);
        
            if ( selectObj == null)
            {
                Debug.LogWarning("ERROR:Obj Is Null !!!");
                continue;
            }
            string path = AssetDatabase.GetAssetPath(selectObj);
            if (path.Length < 1 || path.EndsWith(".prefab") == false)
            {
                Debug.LogWarning("ERROR:Folder=" + path);
            }
            else
            {
                
                Debug.Log("Selected Folder=" + path);
                GameObject clone = GameObject.Instantiate(selectObj) as GameObject;
                Replace(clone, replace);
                SaveDealFinishPrefab(clone, path);
                GameObject.DestroyImmediate(clone);
                Debug.Log("Connect Font Success=" + path);
            }
        }
    }

    private static void Replace(GameObject clone, Font font)
    {
        UnityEngine.UI.Text[] labels = clone.GetComponentsInChildren<UnityEngine.UI.Text>(true);
        foreach (UnityEngine.UI.Text label in labels)
        {
            if (label.font == null || (label.font.dynamic && label.font.name == "Arial"))
            {
                label.font = font;
            }
        }
    }

    private static void CorrectionPublicFont(Font replace, Font matching)
    {
        if (replace == null)
        {
            Debug.LogError("Select Font Is Null...");
            return;
        }
        else
        {
            foreach (string path in lstPrefabPath)
            {
                ChangeFontByPath(path, replace, matching);
            }
            AssetDatabase.Refresh();
        }
    }
  
}
