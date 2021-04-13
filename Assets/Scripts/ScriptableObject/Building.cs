using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Building : ScriptableObject
{
    [ReadOnly]
    public float calculatedHeight;

    [SerializeProperty("prefab")]
    public GameObject _prefab;
    public GameObject prefab
    {
        get => _prefab;
        set
        {
            _prefab = value;
            calculatedHeight = calculateHeight();
        }
    }

    public Vector2 gridSize;
    public float constructionTime;
    public ItemQuantity[] constructionCost;

    public abstract System.Type GetComponentType();
    public abstract void ConfigureComponent(Component component);

    private float calculateHeight()
    {
        var bounds = new Bounds(prefab.transform.position, Vector3.zero);
        foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds.max.y;
    }
}