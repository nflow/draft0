using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBoxControl : MonoBehaviour
{
    public Transform selectionSystem;

    private void Start() {
        var eventSystem = selectionSystem.GetComponent<UISelectionSystem>();
        eventSystem.OnHoverTargetChanged += handleHoverChange;
    }

    private void handleHoverChange(object sender, EntityChangedEventArgs args)
    {
        var rect = GetComponent<RectTransform>().rect;
        var position = args.entity.transform.position;

        return new Vector3(rect.width / 2 + position.x, rect.height / 2 + position.y, position.z);
    }
}
