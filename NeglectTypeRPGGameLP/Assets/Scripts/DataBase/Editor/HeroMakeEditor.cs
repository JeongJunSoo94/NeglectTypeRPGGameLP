using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;

public class HeroMakeEditor : EditorWindow
{
    GoogleSheetManager sheet;

    HeroInfo info;

    [MenuItem("MakeData/HeroMakeEditor")]
    static void OpenWindow()
    {
        HeroMakeEditor window = GetWindow<HeroMakeEditor>(typeof(Cubemap));
        window.minSize = new Vector2(100,100);
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

        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        rootVisualElement.Add(splitView);

        var leftPane = new TwoPaneSplitView(0, 15, TwoPaneSplitViewOrientation.Vertical);

        var leftTopPane = new VisualElement();

        leftPane.Add(leftTopPane);
        leftTopPane.style.backgroundColor = Color.black;
        VisualElement label = new Label("무수한 버튼들");
        leftTopPane.Add(label);

        var leftBottomPane = new VisualElement();
        Button JoinButton =(Button) CreateButton("데이터 불러오기", () => { CallData(); });
        Button SaveDataButton = (Button)CreateButton("에셋으로 데이터 저장", () => { CreateScriptableObject(); });
        leftBottomPane.Add(JoinButton);
        leftBottomPane.Add(SaveDataButton);

        leftPane.Add(leftBottomPane);

        splitView.Add(leftPane);

        var rightPane = new VisualElement();
        splitView.Add(rightPane);
    }

    private VisualElement CreateButton(string text, Action action)
    {
        return new Button(action) { text=text};
    }

    public void CreateScriptableObject()
    {
        AssetDatabase.CreateAsset(CreateInstance<HeroStat>(), "Asset/Resources/Data"+ info.Name + ".asset");
    }

    public void CallData()
    {
        if (sheet == null)
        {
            sheet = new GoogleSheetManager();
        }
        sheet.CreatePost(CreateWWWForm());
    }

    public WWWForm CreateWWWForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("name", "");

        return form;
    }
}
