using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePool : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    public event Action<Base, Unit> BaseBuild;

    public void OnGoToBuild(Unit unit)
    {
        unit.ReadyToBuild += OnReadyToBuild;
    }

    private void OnReadyToBuild(Vector3 position, Unit unit)
    {
        unit.ReadyToBuild -= OnReadyToBuild;
        Base newBase = Instantiate(_basePrefab, position, _basePrefab.transform.rotation);
        BaseBuild?.Invoke(newBase, unit);
    }
}
