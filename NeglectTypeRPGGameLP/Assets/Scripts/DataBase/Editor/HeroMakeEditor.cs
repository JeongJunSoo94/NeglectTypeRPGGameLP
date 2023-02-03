using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;
using NeglectTypeRPG;

public class HeroMakeEditor : EditorWindow
{
    List<List<string[]>> heroCache = new List<List<string[]>>();
    VisualElement splitPane;
    VisualElement textPane;
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
        Rect rect = rootVisualElement.contentRect;
        splitPane = new TwoPaneSplitView(0, rect.height*0.5f, TwoPaneSplitViewOrientation.Vertical);

        rootVisualElement.Add(splitPane);


        var buttonPane = new VisualElement();

        Button JoinButton = (Button)CreateButton("데이터 불러오기", () => { CallData(); });
        Button SaveDataButton = (Button)CreateButton("에셋으로 데이터 저장", () => { CreateScriptableObjects(); });
        Button cacheDataButton = (Button)CreateButton("캐시 초기화", () => {
            heroCache.Clear();
            label.text = "초기화했습니다.";
            ColorChange(textPane, Color.gray);
        });
        Button cacheDataLenButton = (Button)CreateButton("캐시 개수", () => {
            label.text = heroCache.Count.ToString();
            ColorChange(textPane, Color.gray);
        });
        buttonPane.Add(JoinButton);
        buttonPane.Add(SaveDataButton);
        buttonPane.Add(cacheDataButton);
        buttonPane.Add(cacheDataLenButton);

        splitPane.Add(buttonPane);

        textPane = new VisualElement();


        label = new Label("엑셀스프레드시트에서 정보를 가져와서 에셋을 만드는 툴입니다.");
        textPane.Add(label);
        splitPane.Add(textPane);
    }

    private VisualElement CreateButton(string text, Action action)
    {
        return new Button(action) { text = text };
    }

    T GetAsset<T>(string path, Type type) where T : ScriptableObject
    {
        T asset = (T)AssetDatabase.LoadAssetAtPath(path, type);
        if (!asset)
        {
            AssetDatabase.CreateAsset(CreateInstance(type), path);
            asset = (T)AssetDatabase.LoadAssetAtPath(path, type);
        }
        return asset;
    }

    public void CreateScriptableObjects()
    {
        string path = "Assets/Datas/AssetsData.asset";
        AssetsData assetsData = GetAsset<AssetsData>(path, typeof(AssetsData));
        assetsData.CreateInit(heroCache);
        EditorUtility.SetDirty(assetsData);

        List<HeroInfo> heroAssets = new List<HeroInfo>();
        List<string[]> cache = heroCache[0];
        for (int j = 0; j < cache.Count; j++)
        {
            path = "Assets/Datas/Heroes/Data/" + cache[j][0] + cache[j][1] + "InfoData.asset";
            HeroInfo asset = GetAsset<HeroInfo>(path, typeof(HeroInfo));
            asset.CreateHeroInfoData(cache[j]);
            heroAssets.Add(asset);
        }
        cache = heroCache[1];
        int index1 = 0,index2 = 0;
        while (index2 != heroAssets.Count)
        {
            heroAssets[index2].CreateHeroStatData(cache[index1]);
            heroAssets[index2].skills.Clear();
            index1 += 5;
            EditorUtility.SetDirty(heroAssets[index2++]);
        }

        List<SkillInfo> skillAssets = new List<SkillInfo>();
        cache = heroCache[2];
        for (int j = 0; j < cache.Count; j++)
        {
            path = "Assets/Datas/Skills/"+ cache[j][0] + cache[j][1] + "Skill.asset";
            SkillInfo asset = GetAsset<SkillInfo>(path, typeof(SkillInfo));
            asset.CreateSkillInfoData(cache[j]);
            skillAssets.Add(asset);
        }
        cache = heroCache[3];
        index1 = 0;
        index2 = 0;
        while (index2 != skillAssets.Count)
        {
            skillAssets[index2].CreateSkillStatData(cache[index1]);
            index1 += 5;
            EditorUtility.SetDirty(skillAssets[index2++]);
        }

        cache = heroCache[4];

        for (int j = 0; j < cache.Count; j++)
        {
            heroAssets[int.Parse(cache[j][0]) - 1].skills.Add(skillAssets[int.Parse(cache[j][1]) - 1]);
        }

        for (int i = 0; i < heroAssets.Count; ++i)
        {
            if (heroAssets[i].Type==0)
                heroAssets[i].normalAttack =skillAssets[22];
            else
                heroAssets[i].normalAttack = skillAssets[23];
        }

        label.text = "데이터를 프로젝트에 저장했습니다.";
        ColorChange(textPane, Color.green);
    }

    public void CallData()
    {
        heroCache.Clear();
        EditorCoroutineUtility.StartCoroutine(GetDataSheet(), this);
    }

    string[] URLNameCache = { "heroInfo", "heroStats", "SkillInfo", "SkillStats", "heroSkillKey" };

    string[] URL = { 
                "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv&gid=1545996921"
            ,   "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv&gid=0"
            ,   "https://docs.google.com/spreadsheets/d/1kI5XpOsfOUDHEUT2X2GwjHT_I8tA1l0RdoHGYlI83p0/export?format=tsv&gid=515130627"
            ,   "https://docs.google.com/spreadsheets/d/1kI5XpOsfOUDHEUT2X2GwjHT_I8tA1l0RdoHGYlI83p0/export?format=tsv&gid=1154008238"
            ,   "https://docs.google.com/spreadsheets/d/1kI5XpOsfOUDHEUT2X2GwjHT_I8tA1l0RdoHGYlI83p0/export?format=tsv&gid=143248180"
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

        label.text = "데이터 받아서 캐시에 담았습니다.";
        ColorChange(textPane, Color.blue);
    }

    public void TSVPasing(string tsv,int index)
    {
        heroCache.Add(new List<string[]>());
        string[] rows = null;
        string[] values = null;

        rows = tsv.Split('\n');
        for (int i = 2; i < rows.Length; i++)
        {
            values = rows[i].Split('\t');
            values[values.Length - 1] = values[values.Length - 1].Split('\r')[0];
            heroCache[index].Add(values);
        }
    }


    public void ColorChange(VisualElement ve,Color color)
    {
        ve.style.backgroundColor = color;
    }

}
