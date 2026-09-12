using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Deliver : MonoBehaviour
{
    private HashSet<Unit> _unitsLeft = new HashSet<Unit>();
    private HashSet<Unit> _unitsWorking = new HashSet<Unit>();
    private HashSet<Coel> _targetCoels = new HashSet<Coel>();
    private HashSet<Coel> _coelsLeft = new HashSet<Coel>();

    public event Action<Unit> UnitComing;
    public event Action<Coel> CoelGetted;
    public event Action<List<Coel>> CoelsNoted;

    public bool UnitsEnough { get; private set; } = true;
    public bool IsNeedGetOnCome { get; private set; } = false;

    public void StartDelivering(List<Coel> coels, List<Unit> units)
    {
        HashSet<Coel> coelsToAdd = new HashSet<Coel>();

        for (int i = 0; i < coels.Count; i++)
        {
            if (!_targetCoels.Contains(coels[i]))
            {
                coelsToAdd.Add(coels[i]);
            }
        }

        _coelsLeft.AddRange(coelsToAdd);
        CoelsNoted?.Invoke(coelsToAdd.ToList());
        Debug.Log(_coelsLeft.Count);

        if (units.Count <= 0)
            return;

        _unitsLeft.AddRange(units);

        int count = Mathf.Min(_coelsLeft.Count, _unitsLeft.Count);

        for (int i = 0; i < count; i++)
        {
            SendUnit(_unitsLeft.First());
            _unitsLeft.Remove(_unitsLeft.First());
        }

        if (_coelsLeft.Count > 0)
        {
            UnitsEnough = false;
        }
        else
        {
            UnitsEnough = true;
        }

        if (_unitsLeft.Count > 0)
        {
            for (int i = 0; i < _unitsLeft.Count; i++)
            {
                UnitComing?.Invoke(_unitsLeft.First());
                _unitsLeft.First().ComeToBase -= OnComeToBase;
            }

            _unitsLeft.Clear();
        }
    }

    public void OnNeedGetOnCome()
    {
        IsNeedGetOnCome = true;
    }

    public void SendUnit(Unit unit)
    {
        if (_coelsLeft.Count > 0)
        {
            unit.ComeToBase -= OnComeToBase;
            unit.ComeToBase += OnComeToBase;
            unit.GoToCoel(_coelsLeft.First());
            _targetCoels.Add(_coelsLeft.First());
            _coelsLeft.Remove(_coelsLeft.First());
            _unitsWorking.Add(unit);
        }     
    }

    private void OnComeToBase(Unit unit, Coel coel)
    {
        if (coel != null)
        {
            _targetCoels.Remove(coel);
            CoelGetted?.Invoke(coel);
        }

        if (!UnitsEnough && !IsNeedGetOnCome)
        {
            SendUnit(unit);
        }
        else
        {
            IsNeedGetOnCome = false;
            unit.ComeToBase -= OnComeToBase;
            _unitsWorking.Remove(unit);
            UnitComing?.Invoke(unit);
        }

        if (_coelsLeft.Count > 0)
        {
            UnitsEnough = false;
        }
        else
        {
            UnitsEnough = true;
        }
    }
}
