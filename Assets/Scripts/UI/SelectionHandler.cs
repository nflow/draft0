using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectionSystem : MonoBehaviour
{
    
    private event EventHandler<EntityChangedEventArgs> OnHoverTargetChanged;

    private event EventHandler<EntityChangedEventArgs> OnSelectedTargetChanged;

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

    private Vector3 calcInfoBoxOffset(Vector3 orig)
    {
        var rect = infoBox.GetComponent<RectTransform>().rect;
        return new Vector3(rect.width / 2 + orig.x, rect.height / 2 + orig.y, orig.z);
    }
}

public class EntityChangedEventArgs : EventArgs
{
    public EntityChangedEventArgs(GameObject entity) {
        this.entity = entity;
    }

    public GameObject entity { get; private set; }
}