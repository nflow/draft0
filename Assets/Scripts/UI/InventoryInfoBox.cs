using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfoBox : MonoBehaviour
{
    public Transform listElementPrefab;
    public Transform listContainer;
    public Transform selectionSystem;

    private void Start()
    {
        selectionSystem.GetComponent<SelectionSystem>().OnHoverEntityEnter += handleHoverChange;
    }

    private void handleHoverChange(object sender, EntityEventArgs args)
    {
        foreach (Transform child in listContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        var ii = args.entity.GetComponent<IInteractableInventory>();
        if (ii != null)
        {
            foreach (var element in ii.inventory.GetList())
            {
                if (element.realAmount > 0)
                {
                    var newElement = Instantiate(listElementPrefab, listContainer);
                    newElement.Find("Amount").GetComponent<TextMeshProUGUI>().SetText(element.realAmount.ToString());
                    newElement.Find("Icon").GetComponent<Image>().sprite = element.item.itemIcon;
                    newElement.Find("Name").GetComponent<TextMeshProUGUI>().SetText(element.item.name);
                }
            }
        }
    }
}
