using UnityEngine;
using UnityEngine.UI;

public class GUIInputHandler : MonoBehaviour
{
    public GameObject placementGrid;

    public void Build(Building building) {
        PlacementGrid script = placementGrid.GetComponent<PlacementGrid>();
        placementGrid.SetActive(!placementGrid.activeSelf);
        script.buildingPattern = building;
    }
}
