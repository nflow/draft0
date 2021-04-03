using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject infoBox;

    void Start()
    {

    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Default")))
        {
            if (hit.collider)
            {
                this.selectedObject = hit.collider.gameObject;
                infoBox.SetActive(true);
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(selectedObject.transform.position);
                infoBox.transform.position = calcInfoBoxOffset(screenPosition);
            }
        }
        else
        {
            infoBox.SetActive(false);
        }
    }

    private Vector3 calcInfoBoxOffset(Vector3 orig)
    {
        var rect = infoBox.GetComponent<RectTransform>().rect;
        return new Vector3(rect.width / 2 + orig.x, rect.height / 2 + orig.y, orig.z);
    }
}
