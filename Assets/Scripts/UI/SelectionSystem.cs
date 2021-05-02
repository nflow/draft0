using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionSystem : MonoBehaviour
{

    private EntityEventArgs entityArgs;
    public event EventHandler<EntityEventArgs> OnHoverEntityEnter;
    public event EventHandler<EntityEventArgs> OnHoverEntityExit;

    public event EventHandler<EntityEventArgs> OnSelectedEntityChanged;

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
            if (hit.collider &&
                hit.collider.tag != Tag.IGNORE_UI_HIT)
            {
                var eventArgs = new EntityEventArgs(hit.collider.gameObject);
                if (Input.GetMouseButtonDown(0)) {
                    OnSelectedEntityChanged?.Invoke(this, eventArgs);
                }

                if (entityArgs is null || !entityArgs.Equals(eventArgs)) {
                    entityArgs = eventArgs;
                    OnHoverEntityEnter?.Invoke(this, entityArgs);
                }
            }
        } else {
            if (entityArgs != null) {
                OnHoverEntityExit?.Invoke(this, entityArgs);
                entityArgs = null;
            }
        }
    }
}

public class EntityEventArgs : EventArgs
{
    public EntityEventArgs(GameObject entity) {
        this.entity = entity;
    }

    public GameObject entity { get; private set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        return entity.Equals(((EntityEventArgs)obj).entity);
    }
}