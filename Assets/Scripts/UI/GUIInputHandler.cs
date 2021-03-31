using UnityEngine;
using UnityEngine.UI;

public class GUIInputHandler : MonoBehaviour
{
    public GameObject placementGrid;

    public void Build(Building building) {
        PlacementGrid script = placementGrid.GetComponent<PlacementGrid>();
        if (building.Equals(script.buildingPattern)) {
            placementGrid.SetActive(!placementGrid.activeSelf);
        } else {
            placementGrid.SetActive(true);
            script.buildingPattern = building;
        }
    }
}
