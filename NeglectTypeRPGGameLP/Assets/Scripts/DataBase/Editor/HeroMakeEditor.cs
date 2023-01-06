using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;

public class HeroMakeEditor : EditorWindow
{
    List<List<string[]>> heroCache = new List<List<string[]>>();
    VisualElement splitPane;
    VisualElement topPane;
    Label label;

    [MenuItem("MakeData/HeroMakeEditor")]
    static void OpenWindow()
    {
        HeroMakeEditor window = GetWindow<HeroMakeEditor>(typeof(Cubemap));
        window.minSize = new Vector2(100, 100);
        window.maxSize = new Vector2(1000, 1000);
        window.Show();
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if (Selection.activeObject is ScriptableObject)
        {
            OpenWindow();
            return true;
        }
        return false;
    }

    public void CreateGUI()
    {
        var allObjectGuids = AssetDatabase.FindAssets("t:Sprite");
        var allObjects = new List<Sprite>();
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        splitPane = new TwoPaneSplitView(0, 15, TwoPaneSplitViewOrientation.Vertical);

        rootVisualElement.Add(splitPane);

        topPane = new VisualElement();

        splitPane.Add(topPane);
        label = new Label("켜졌습니다.");
        topPane.Add(label);

        var BottomPane = new VisualElement();

        Button JoinButton = (Button)CreateButton("데이터 불러오기", () => { CallData(); });
        Button SaveDataButton = (Button)CreateButton("에셋으로 데이터 저장", () => { CreateScriptableObjects(); });
        Button cacheDataButton = (Button)CreateButton("캐시 초기화", () => { heroCache.Clear(); });
        Button cacheDataLenButton = (Button)CreateButton("캐시 개수", () => { Debug.Log(heroCache.Count); });
        BottomPane.Add(JoinButton);
        BottomPane.Add(SaveDataButton);
        BottomPane.Add(cacheDataButton);
        BottomPane.Add(cacheDataLenButton);

        splitPane.Add(BottomPane);

    }

    private VisualElement CreateButton(string text, Action action)
    {
        return new Button(action) { text = text };
    }

    public void CreateScriptableObjects()
    {
        for (int i = 0; i < heroCache.Count; i++)
        {
            List<string[]> cache = heroCache[i];
            if (i == 0)
            {
                for (int j = 0; j < cache.Count; j++)
                {
                    string path = "Assets/Heroes/Data/" + cache[j][1] + "InfoData.asset";
                    HeroInfo asset = (HeroInfo)AssetDatabase.LoadAssetAtPath(path, typeof(HeroInfo));
                    if (!asset)
                    {
                        AssetDatabase.CreateAsset(CreateInstance<HeroInfo>(), path);
                        asset = (HeroInfo)AssetDatabase.LoadAssetAtPath(path, typeof(HeroInfo));
                    }
                    asset.CreateHeroInfoData(asset, cache[j]);
                    EditorUtility.SetDirty(asset);
                }
            }
            else if (i == 1)
            {
                for (int j = 0; j < cache.Count; j++)
                {
                    string path = "Assets/Heroes/Data/" + cache[j][0] + "StatData.asset";
                    HeroStat asset = (HeroStat)AssetDatabase.LoadAssetAtPath(path, typeof(HeroStat));
                    if (!asset)
                    {
                        AssetDatabase.CreateAsset(CreateInstance<HeroStat>(), path);
                        asset = (HeroStat)AssetDatabase.LoadAssetAtPath(path, typeof(HeroStat));
                    }
                    asset.CreateHeroStatData(asset, cache[j]);
                    EditorUtility.SetDirty(asset);
                }
            }
            else if (i == 2)
            {
                for (int j = 0; j < cache.Count; j++)
                {
                    string path = "Assets/Heroes/Data/FactionInfo.asset";
                    HeroStat asset = (HeroStat)AssetDatabase.LoadAssetAtPath(path, typeof(HeroStat));
                    if (!asset)
                    {
                        AssetDatabase.CreateAsset(CreateInstance<HeroStat>(), path);
                        asset = (HeroStat)AssetDatabase.LoadAssetAtPath(path, typeof(HeroStat));
                    }
                    asset.CreateHeroStatData(asset, cache[j]);
                    EditorUtility.SetDirty(asset);
                }
            }
        }
        label.text = "데이터 저장했쪄염 뿌우";
        ColorChange(topPane, Color.green);
    }

    public void CallData()
    {
        heroCache.Clear();
        EditorCoroutineUtility.StartCoroutine(GetDataSheet(), this);
    }
    string[] URL = { 
                "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv&gid=1545996921"
            ,   "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv&gid=0"
    };
    IEnumerator GetDataSheet()
    {
        UnityWebRequest www;
        string data;
        for (int i = 0; i < URL.Length; i++)
        {
            www = UnityWebRequest.Get(URL[i]);
            yield return www.SendWebRequest();

            data = www.downloadHandler.text;
            TSVPasing(data, i);
        }

        label.text = "데이터 불러왔쪄염 뿌우";
        ColorChange(topPane, Color.blue);
    }

    public void TSVPasing(string tsv,int index)
    {
        heroCache.Add(new List<string[]>());
        string[] rows = null;
        string[] values = null;

        rows = tsv.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            values = rows[i].Split('\t');
            heroCache[index].Add(values);
        }
    }


    public void ColorChange(VisualElement ve,Color color)
    {
        ve.style.backgroundColor = color;
    }

}
