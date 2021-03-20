using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Recipe))]
public class RecipePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        var allRecipes = Resources.FindObjectsOfTypeAll<Recipe>().ToList();
        var selectedIndex = 0;
        if (!property.objectReferenceValue) {
            property.objectReferenceValue = allRecipes[selectedIndex];
        } else {
            selectedIndex = allRecipes.FindIndex(e => e == property.objectReferenceValue);
        }

        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUI.Popup(position, selectedIndex, allRecipes.Select(e => new GUIContent(e.name)).ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            property.objectReferenceValue = allRecipes[selectedIndex];
        }

        EditorGUI.EndProperty();
    }
}