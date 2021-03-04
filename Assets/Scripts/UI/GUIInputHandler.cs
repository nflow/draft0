using UnityEngine;
using UnityEngine.UI;

public class GUIInputHandler : MonoBehaviour
{
    public GameObject buildMenu;
    public GameObject placementGrid;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            ToggleBuildMenu();
        }
    }

    public void ToggleBuildMenu() {
        buildMenu.SetActive(!buildMenu.activeSelf);
    }

    public void Build(Building building) {
        PlacementGrid script = placementGrid.GetComponent<PlacementGrid>();
        placementGrid.SetActive(!placementGrid.activeSelf);
        script.buildingPattern = building;
    }
}
