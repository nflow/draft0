using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "New ItemPreset", menuName = "ItemPreset", order = 1)]
public class StoragePreset : ScriptableObject
{
    public List<ItemQuantity> itemPreset;

    public StoragePreset() {
        itemPreset = new List<ItemQuantity>();
    }
}