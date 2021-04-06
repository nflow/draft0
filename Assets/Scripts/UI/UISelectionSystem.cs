using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectionSystem : MonoBehaviour
{

    public event EventHandler<EntityChangedEventArgs> OnHoverTargetChanged;

    public event EventHandler<EntityChangedEventArgs> OnSelectedTargetChanged;

    private void Update()
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
                var eventArgs = new EntityChangedEventArgs(hit.collider.gameObject);
                if (Input.GetMouseButtonDown(0)) {
                    OnSelectedTargetChanged?.Invoke(this, eventArgs);
                } else {
                    OnHoverTargetChanged?.Invoke(this, eventArgs);
                }
            }
        }
    }
}

public class EntityChangedEventArgs : EventArgs
{
    public EntityChangedEventArgs(GameObject entity) {
        this.entity = entity;
    }

    public GameObject entity { get; private set; }
}