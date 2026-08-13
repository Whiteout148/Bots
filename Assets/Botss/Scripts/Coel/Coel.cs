using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coel : MonoBehaviour, IPooleableObject<Coel>
{
    public event Action<Coel> NeedToRelease;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PooleableTrigger>(out _))
        {
            NeedToRelease?.Invoke(this);
        }
    }
}
