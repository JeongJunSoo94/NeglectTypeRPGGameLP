using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;
using UnityEngine.Networking;
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

    public void CreateAsset<T>(List<T> list, List<string[]> cache1, List<string[]> cache2, string prev, string assetName, Type type) where T : ScriptableObject
    {
        string path="";
        for (int j = 0; j < cache1.Count; j++)
        {
            path = prev + cache1[j][0] + cache1[j][1] + assetName;
            T asset = GetAsset<T>(path, type);
            if (type == typeof(HeroInfo))
            {
                HeroInfo.CreateHeroInfoData(asset as HeroInfo, cache1[j]);
            }
            else if(type == typeof(SkillInfo))
            {
                SkillInfo.CreateSkillInfoData(asset as SkillInfo, cache1[j]);
            }
            list.Add(asset);
        }
        int index1 = 0, index2 = 0;

        while (index2 != list.Count)
        {
            if (type == typeof(HeroInfo))
            {
                HeroInfo temp = list[index2] as HeroInfo;
                HeroInfo.CreateHeroStatData(temp, cache2[index1]);
                temp.skills.Clear();
                EditorUtility.SetDirty(temp);
            }
            else if (type == typeof(SkillInfo))
            {
                //SkillInfo.CreateSkillStatData(list[index2] as SkillInfo,);
                EditorUtility.SetDirty(list[index2]);
            }
            index1 += 5;
            ++index2;
        }
    }

    public void CreateScriptableObjects()
    {
        string path = "Assets/Datas/AssetsData.asset";
        AssetsData assetsData = GetAsset<AssetsData>(path, typeof(AssetsData));
        assetsData.CreateInit(heroCache);
        EditorUtility.SetDirty(assetsData);

        path = "Assets/Datas/SkillsData.asset";
        SkillsData skillsData = GetAsset<SkillsData>(path, typeof(SkillsData));
        skillsData.CreateSkillStatsInit(heroCache[3]);
        skillsData.CreateSkillDamageStatsInit(heroCache[5]);
        EditorUtility.SetDirty(assetsData);

        List<HeroInfo> heroAssets = new List<HeroInfo>();
        CreateAsset(heroAssets, heroCache[0], heroCache[1], "Assets/Datas/Heroes/Data/", "InfoData.asset", typeof(HeroInfo));
        List<SkillInfo> skillAssets = new List<SkillInfo>();
        CreateAsset(skillAssets, heroCache[2], heroCache[3], "Assets/Datas/Skills/", "Skill.asset", typeof(SkillInfo));
        
        List<string[]> cache = heroCache[4];

        for (int j = 0; j < cache.Count; j++)
        {
            heroAssets[int.Parse(cache[j][0]) - 1].skills.Add(skillAssets[int.Parse(cache[j][1]) - 1]);
        }

        for (int i = 0; i < heroAssets.Count; ++i)
        {
            if (heroAssets[i].Type!=0)
                heroAssets[i].normalAttack =skillAssets[22];
            else
                heroAssets[i].normalAttack = skillAssets[23];
        }

        label.text = "데이터를 프로젝트에 저장했습니다.";
        ColorChange(textPane, Color.blue);
    }

    public void CallData()
    {
        label.text = "데이터를 불러오고 있습니다.";
        ColorChange(textPane, Color.black);
        heroCache.Clear();
        EditorCoroutine.StartCoroutine(GetDataSheet());
    }

    string[] URLNameCache = { "heroInfo", "heroStats", "SkillInfo", "SkillStats", "heroSkillKey", "SkillDamageStats" };

    string[] URL = { 
                "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv&gid=1545996921"
            ,   "https://docs.google.com/spreadsheets/d/1MdMnjWtkRkXVJ-RtGtABf4rEeAfjpwPzvTT-iH1J1is/export?format=tsv&gid=0"
            ,   "https://docs.google.com/spreadsheets/d/1kI5XpOsfOUDHEUT2X2GwjHT_I8tA1l0RdoHGYlI83p0/export?format=tsv&gid=515130627"
            ,   "https://docs.google.com/spreadsheets/d/1kI5XpOsfOUDHEUT2X2GwjHT_I8tA1l0RdoHGYlI83p0/export?format=tsv&gid=1154008238"
            ,   "https://docs.google.com/spreadsheets/d/1kI5XpOsfOUDHEUT2X2GwjHT_I8tA1l0RdoHGYlI83p0/export?format=tsv&gid=143248180"
            ,   "https://docs.google.com/spreadsheets/d/1kI5XpOsfOUDHEUT2X2GwjHT_I8tA1l0RdoHGYlI83p0/export?format=tsv&gid=930504114"
    };

    IEnumerator GetDataSheet()
    {
        UnityWebRequest www;
        string data;
        for (int i = 0; i < URL.Length; i++)
        {
            www = UnityWebRequest.Get(URL[i]);
            yield return www.SendWebRequest();

            while (!www.isDone)
                yield return null;

            if (www.isDone)
            {
                data = www.downloadHandler.text;
                TSVPasing(data, i);
            }

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
