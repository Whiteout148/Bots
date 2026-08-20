using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoelPool : Pool<Coel>
{
    public int Count => ActiveItems.Count;

    public override Coel Get()
    {
        Coel coel = base.Get();
        coel.gameObject.SetActive(true);

        return coel;
    }

    public override void Release(Coel itemToRelease)
    {
        base.Release(itemToRelease);
        itemToRelease.gameObject.SetActive(false);
        itemToRelease.transform.parent = null;
    }

    protected override Coel Create()
    {
        Coel coel = Instantiate(Prefab);
        coel.gameObject.SetActive(false);

        return coel;
    }
}
