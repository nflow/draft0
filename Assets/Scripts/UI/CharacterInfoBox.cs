using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoBox : MonoBehaviour
{
    public Transform selectionSystem;
    public TextMeshProUGUI ageValueField;

    private void Start()
    {
        selectionSystem.GetComponent<SelectionSystem>().OnHoverEntityEnter += handleHoverChange;
    }

    private void handleHoverChange(object sender, EntityEventArgs args)
    {        
        var entity = args.entity.GetComponent<Character>();
        gameObject.SetActive(entity != null);
        if (entity != null)
        {
            ageValueField.SetText(entity.age.ToString());       
        }
            
    }
}
