using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coel : MonoBehaviour, IPooleableObject<Coel>
{
    public event Action<Coel> NeedToRelease;

    public bool IsTaked { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PooleableTrigger>(out _))
        {
            NeedToRelease?.Invoke(this);
            IsTaked = false;
        }

        if (other.gameObject.TryGetComponent<Unit>(out _))
        {
            IsTaked = true;
        }
    }
}
