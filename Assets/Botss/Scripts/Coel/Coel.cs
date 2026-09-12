using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coel : MonoBehaviour, IPooleableObject<Coel>
{
    public event Action<Coel> NeedToRelease;

    public void SetToRelease()
    {
        NeedToRelease?.Invoke(this);
    }
}
