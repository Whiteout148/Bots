using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPool : Pool<Unit>
{
    public override Unit Get()
    {
        Unit unit = base.Get();
        unit.gameObject.SetActive(true);

        return unit;
    }

    protected override Unit Create()
    {
        Unit unit = Instantiate(Prefab);
        unit.gameObject.SetActive(false);

        return unit;
    }
}
