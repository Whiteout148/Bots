using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Base : MonoBehaviour, ICantCoelSpawnable
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private WareHouse _wareHouse;
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Deliver _deliver;

    private List<Unit> _freeUnits = new List<Unit>();
    private Coroutine _deliveringCoroutine;

    private void OnEnable()
    {
        _scanner.CoelsFounded += OnScanEnd;
        _deliver.UnitComing += OnUnitCome;
        _deliver.CoelGetted += _wareHouse.AddCoel;
    }

    private void OnDisable()
    {
        _scanner.CoelsFounded -= OnScanEnd;
        _deliver.UnitComing -= OnUnitCome;
        _deliver.CoelGetted -= _wareHouse.AddCoel;
    }

    private void Start()
    {
        _freeUnits.AddRange(_units);
    }

    private void OnScanEnd(List<Coel> coels)
    {
        List<Unit> unitsToSend = _freeUnits.Where(unit => unit.State == UnitState.Free).Take(coels.Count).ToList();

        if (unitsToSend.Count <= 0)
            return;

        Debug.Log("scan end");
        _deliver.StartDelivering(coels, unitsToSend);
    }

    private void OnUnitCome(Unit unit)
    {
        _freeUnits.Add(unit);
    }
}
