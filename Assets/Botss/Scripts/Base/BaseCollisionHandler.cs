using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionHandler : MonoBehaviour
{
    public event Action<Coel> GettedCoel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Coel coel))
        {
            GettedCoel?.Invoke(coel);
        }
    }
}
