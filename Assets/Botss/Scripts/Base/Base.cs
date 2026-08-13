using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private BaseCollisionHandler _collisionHandler;
    [SerializeField] private WareHouse _wareHouse;
    [SerializeField] private List<Unit> _units;

    private void OnEnable()
    {
        _collisionHandler.GettedCoel += _wareHouse.AddCoel;
    }

    private void OnDisable()
    {
        _collisionHandler.GettedCoel -= _wareHouse.AddCoel;
    }
}
