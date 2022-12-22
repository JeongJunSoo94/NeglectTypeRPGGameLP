using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class HeroMakeEditor : EditorWindow
{
    [MenuItem("MakeData/HeroMakeEditor")]
    static void Init()
    {
        HeroMakeEditor window = GetWindow<HeroMakeEditor>(typeof(Cubemap));
        window.minSize = new Vector2(100,100);
        window.maxSize = new Vector2(1000, 1000);
        window.Show();
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

        leftPane.StretchToParentSize();

        var leftTopPane = new VisualElement();

        leftPane.Add(leftTopPane);
        leftTopPane.style.backgroundColor = Color.black;
        VisualElement label = new Label("Inspector");
        leftTopPane.Add(label);

        var leftBottomPane = new VisualElement();
        VisualElement button = new Button();
        leftBottomPane.Add(button);
        leftPane.Add(leftBottomPane);

        splitView.Add(leftPane);


        var rightPane = new VisualElement();
        splitView.Add(rightPane);
    }


}
