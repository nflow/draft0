using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{
    private const int MAX_SLIDE = 100;
    private Item itemField;
    private int amountField = 0;
    private Recipe recipe;

    private void OnEnable() {
        recipe = (Recipe)target;
    }

    private void OnDisable() {
        EditorUtility.SetDirty(recipe);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh ();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Required Item(s):");
        foreach (var component in recipe.requiredItems)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(component.item.itemIcon, GUILayout.MaxWidth(16), GUILayout.MaxHeight(16));
            GUILayout.Label(component.item + ": ");
            int value = EditorGUILayout.IntSlider(component.amount, 1, MAX_SLIDE);
            if (value != component.amount)
            {
                component.amount = value;
                return;
            }

            if (GUILayout.Button("Delete"))
            {
                recipe.RemoveRequirement(component.item);
                return;
            }
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        GUILayout.Label("Creates Item(s):");
        foreach (var component in recipe.producedItems)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(component.item.itemIcon, GUILayout.MaxWidth(16), GUILayout.MaxHeight(16));
            GUILayout.Label(component.item + ": ");
            int value = EditorGUILayout.IntSlider(component.amount, 1, MAX_SLIDE);
            if (value != component.amount)
            {
                component.amount = value;
                return;
            }
            if (GUILayout.Button("Delete"))
            {
                recipe.RemoveProduct(component.item);
                return;
            }
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Item: ");
        itemField = (Item)EditorGUILayout.ObjectField(itemField, typeof(Item), true);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Amount: ");
        amountField = EditorGUILayout.IntSlider(amountField, 1, MAX_SLIDE);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (!recipe.ContainsRequirement(itemField) && GUILayout.Button("Add Requirement"))
        {
            recipe.AddRequirement(itemField, amountField);
        }
        if (!recipe.ContainsProduct(itemField) && GUILayout.Button("Add Product"))
        {
            recipe.AddProduct(itemField, amountField);
        }
        GUILayout.EndHorizontal();
    }
}