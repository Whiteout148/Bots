using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Deliver : MonoBehaviour
{
    private Coroutine _deliveringCoroutine;
    private HashSet<Unit> _units = new HashSet<Unit>();
    private HashSet<Unit> _unitsTaking = new HashSet<Unit>();
    private HashSet<Coel> _targetCoels = new HashSet<Coel>();
    private HashSet<Coel> _coelsToTake = new HashSet<Coel>();

    public event Action<Unit> UnitComing;
    public event Action<Coel> CoelGetted;

    public void StartDelivering(List<Coel> coels, List<Unit> units)
    {    
        _units.AddRange(units);
        _coelsToTake.AddRange(coels);

        int count = Mathf.Min(_coelsToTake.Count, _units.Count);

        for (int i = 0; i < count; i++)
        {
            _units.First().ComeToBase += OnComeToBase;
            _units.First().CoelCome += OnCoelCome;
            _units.First().GoToCoel(_coelsToTake.First());
            _targetCoels.Add(_coelsToTake.First());
            _unitsTaking.Add(_units.First());
            _coelsToTake.Remove(_coelsToTake.First());
            _units.Remove(_units.First());
        }

        if (_units.Count > 0)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                UnitComing?.Invoke(_units.First());
                _units.First().ComeToBase -= OnComeToBase;
                _units.First().CoelCome -= OnCoelCome;
            }

            _units.Clear();
        }

        Debug.Log("Delivering start " + _targetCoels.Count + " units " + _unitsTaking.Count + " coels no target: " + _coelsToTake.Count);
    }

    private void OnComeToBase(Unit unit)
    {
        if (_coelsToTake.Count > 0)
        {
            unit.GoToCoel(_coelsToTake.First());
            _targetCoels.Add(_coelsToTake.First());
            _coelsToTake.Remove(_coelsToTake.First());
        }
        else
        {
            UnitComing?.Invoke(unit);
            unit.ComeToBase -= OnComeToBase;
            unit.CoelCome -= OnCoelCome;
        }
    }

    private void OnCoelCome(Coel coel)
    {
        _targetCoels.Remove(coel);
        CoelGetted?.Invoke(coel);
    }
}
