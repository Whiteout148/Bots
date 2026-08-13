using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitTowardsMover _mover;
    [SerializeField] private Vector3 _basePlace;

    public void GoToCoel(Vector3 coelPlace)
    {
        _mover.MoveTo(coelPlace);
    }
}
