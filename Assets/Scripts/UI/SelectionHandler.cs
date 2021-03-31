using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour
{
    private GameObject selectedObject;

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
            if (hit.collider && Input.GetMouseButtonDown(0))
            {
                this.selectedObject = hit.collider.gameObject;
            }
        }
    }
}
