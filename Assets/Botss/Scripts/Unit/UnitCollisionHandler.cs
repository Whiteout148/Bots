using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollisionHandler : CollisionHandler
{
    public event Action ComeOnBase;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.TryGetComponent<Base>(out _))
        {
            ComeOnBase?.Invoke();
        }
    }
}
