using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VilligerInfoBox : MonoBehaviour
{
    public Transform selectionSystem;

    private void Start()
    {
        selectionSystem.GetComponent<SelectionSystem>().OnHoverEntityEnter += handleHoverChange;
    }

    private void handleHoverChange(object sender, EntityEventArgs args)
    {        
        var entity = args.entity.GetComponent<Reproduction>();
        gameObject.SetActive(entity != null);
        if (entity != null)
        {
            transform.Find("Age").GetComponent<TextMeshProUGUI>().SetText(entity.age.ToString());       
        }
            
    }
}
