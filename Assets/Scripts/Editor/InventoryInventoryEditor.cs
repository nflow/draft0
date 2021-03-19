using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
public class InventoryEditor : Editor
{

    private const int MAX_SLIDE = 100;
    private Item itemField;
    private int amountField = 0;

    public override void OnInspectorGUI()
    {
        if (target is IInteractableInventory)
        {
            var gameObject = target as IInteractableInventory;
            GUILayout.Label("Inventory Content");

            if (gameObject != null && gameObject.inventory != null)
            {
                #region Inventory View
                var inventory = gameObject.inventory.GetList();
                if (inventory != null)
                {
                    foreach (var element in inventory)
                    {
                        #region Item Element
                        GUILayout.BeginHorizontal();

                        GUILayout.Label(element.item.itemIcon, GUILayout.MaxWidth(16), GUILayout.MaxHeight(16));
                        GUILayout.Label(element.item.name + ": ");

                        GUILayout.BeginVertical();
                        GUILayout.Label("Real Value: " + element.realAmount);
                        GUILayout.Label("Virtual Value: " + element.virtualAmount);
                        GUILayout.EndVertical();

                        GUILayout.EndHorizontal();
                        #endregion

                    }
                    EditorUtility.SetDirty(target);
                }
                #endregion

                EditorGUILayout.Space();
                #region Add to inventory UI

                GUILayout.BeginHorizontal();
                GUILayout.Label("Item: ");
                itemField = (Item)EditorGUILayout.ObjectField(itemField, typeof(Item), true);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Amount: ");
                amountField = EditorGUILayout.IntSlider(amountField, 1, MAX_SLIDE);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add Item"))
                {
                    gameObject.inventory.Add(itemField, amountField);
                }
                GUILayout.EndHorizontal();
                #endregion
            }
        }

        base.OnInspectorGUI();
    }
}