using UnityEngine;
using UnityEngine.UI;

public class GUIInputHandler : MonoBehaviour
{
    public Button buildButton;

    public GameObject buildMenu;

    void Start()
    {
        buildButton.onClick.AddListener(ToggleBuildMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            ToggleBuildMenu();
        }
    }

    private void ToggleBuildMenu() {
        buildMenu.SetActive(!buildMenu.activeSelf);
    }
}
