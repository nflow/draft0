using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ItemQuantity))]
public class LevelScriptEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        ItemQuantity myTarget = (ItemQuantity) target;

        //myTarget.amount = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
}