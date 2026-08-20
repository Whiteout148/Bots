using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<Coel> GettedCoel;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Coel coel))
        {
            GettedCoel?.Invoke(coel);
        }
    }
}
