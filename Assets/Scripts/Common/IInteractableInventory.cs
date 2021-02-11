using UnityEngine;

public interface IInteractableInventory
{
    Transform transform { get; }
    IInventory inventory { get; }
}