using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooleableObject<T> where T : class
{
    public event Action<T> NeedToRelease;
}
