using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public event Action Selected;
    public event Action Deselected;

    void OnSelect();
    void OnDeselect();
}
