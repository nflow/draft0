using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Production", menuName = "Building/Production", order = 1)]
public class ProductionBuilding : Building
{

    public Recipe recipe;
    
    public override System.Type GetComponentType() {
        return typeof(Production);
    }
    public override void ConfigureComponent(Component component) {
        if (!(component is Production)) {
            throw new System.InvalidOperationException();
        }

        var prod = component as Production;
        prod.recipe = recipe;
    }
}
