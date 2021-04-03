using System.Linq;

using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ItemQuantity))]
public class ItemQuantityPropertyDrawer : PropertyDrawer
{

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var itemField = new PropertyField(property.FindPropertyRelative("item"));
        var amountField = new PropertyField(property.FindPropertyRelative("amount"));

        // Add fields to the container.
        container.Add(itemField);
        container.Add(amountField);

        return container;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var itemRect = new Rect(position.x, position.y, 100, position.height);
        var amountRect = new Rect(position.x + 120, position.y, position.width, position.height);

        var allItems = Resources.FindObjectsOfTypeAll<Item>().ToList();

        var item = property.FindPropertyRelative("item");
        var selectedIndex = 0;
        if (!item.objectReferenceValue) {
            item.objectReferenceValue = allItems[selectedIndex];
        } else {
            selectedIndex = allItems.FindIndex(e => e == item.objectReferenceValue);
        }

        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUI.Popup(itemRect, selectedIndex, allItems.Select(e => new GUIContent(e.name, e.itemIcon.texture)).ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            item.objectReferenceValue = allItems[selectedIndex];
        }
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);

        EditorGUI.EndProperty();
    }
}