using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBoxControl : MonoBehaviour
{
    public Transform selectionSystem;

    private void Start() {
        var eventSystem = selectionSystem.GetComponent<SelectionSystem>();
        eventSystem.OnHoverEntityEnter += handleHoverEnter;
        eventSystem.OnHoverEntityExit += handleHoverExit;
    }

    private void OnDestroy()
    {
        var eventSystem = selectionSystem.GetComponent<SelectionSystem>();
        eventSystem.OnHoverEntityEnter -= handleHoverEnter;
        eventSystem.OnHoverEntityExit -= handleHoverExit;
    }

    private void handleHoverEnter(object sender, EntityEventArgs args)
    {
        gameObject.SetActive(true);
        var rect = GetComponent<RectTransform>().rect;
        transform.position = Camera.main.WorldToScreenPoint(args.entity.transform.position);
    }

    private void handleHoverExit(object sender, EntityEventArgs args)
    {
        gameObject.SetActive(false);
    }
}
