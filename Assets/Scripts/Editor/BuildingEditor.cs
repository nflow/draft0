using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{
    public void OnEnable()
    {
        starSystem = (Building)target;
        rootElement = new VisualElement();

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/BuildingEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        rootElement.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/BuildingEditor.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        rootElement.Add(labelWithStyle);
    }

    public override VisualElement CreateInspectorGUI()
    {
    }
}