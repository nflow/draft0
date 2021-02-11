using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Building : ScriptableObject
{
    public GameObject prefab;
    public Vector2 gridSize;
    public float constructionTime;
    public List<ItemQuantity> constructionCost;

    public Building() {
        constructionCost = new List<ItemQuantity>();
    }

    public abstract System.Type GetComponentType();
    public abstract void ConfigureComponent(Component component);
}