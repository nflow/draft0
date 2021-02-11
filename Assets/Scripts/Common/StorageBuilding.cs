using UnityEngine;

[CreateAssetMenu(fileName = "New Storage", menuName = "Building/Storage", order = 1)]
public class StorageBuilding : Building
{
    public override System.Type GetComponentType() {
        return typeof(Storage);
    }
    public override void ConfigureComponent(Component component) {}
}