using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Base : MonoBehaviour, ICantCoelSpawnable, ISelectable
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private List<Coel> _coels;
    [SerializeField] private Deliver _deliver;
    [SerializeField] private Transform _place;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BaseState _state;
    [SerializeField] private int _coelsToAddUnit;
    [SerializeField] private int _coelsToColonize;
    [SerializeField] private Banner _banner;

    private bool _bannerSetted;
    [SerializeField] private List<Unit> _units = new List<Unit>();
    [SerializeField] private List<Unit> _freeUnits = new List<Unit>();
    private Coroutine _waitingToFirstFreeUnit;
    private Unit _builderUnit;

    public event Func<Unit> NeedUnit;
    public event Action<Banner> BannerSended;
    public event Action<Unit> GoingBuild;
    public event Action Selected;
    public event Action Deselected;
    public event Action<int> FreeUnitsChanged;
    public event Action<int> UnitsChanged;
    public event Action<int> CoelsChanged;

    private void OnEnable()
    {
        _scanner.CoelsFounded += OnScanEnd;
        _deliver.UnitComing += AddFreeUnit;
        _deliver.CoelGetted += OnCoelGet;
    }

    private void OnDisable()
    {
        _scanner.CoelsFounded -= OnScanEnd;
        _deliver.UnitComing -= AddFreeUnit;
        _deliver.CoelGetted -= OnCoelGet;
    }

    public void Init()
    {
        _state = BaseState.Extension;
        _freeUnits.AddRange(_units);
        FreeUnitsChanged?.Invoke(_freeUnits.Count);
        UnitsChanged?.Invoke(_units.Count);
        CoelsChanged?.Invoke(_coels.Count);
    }

    public void OnSelect()
    {
        if (!_bannerSetted)
        {
            BannerSended?.Invoke(_banner);
        }

        Selected?.Invoke();
    }

    public void OnDeselect()
    {
        Deselected?.Invoke();
    }

    public void AddUnit(Unit unit)
    {
        if (!_units.Contains(unit))
        {
            unit.Init(_place);
            _units.Add(unit);
            UnitsChanged?.Invoke(_units.Count);
        }
    }

    public void OnBannerSet()
    {
        if (!_bannerSetted)
        {
            _state = BaseState.Colonization;
            _bannerSetted = true;
        }
        else
        {
            if (_builderUnit != null)
            {
                _builderUnit.GoToBuild(_banner);
            }
        }
    }

    private void OnReadyToBuild(Vector3 newPosition, Unit unit)
    {
        _units.Remove(unit);
        UnitsChanged?.Invoke(_units.Count);
    }

    private void OnScanEnd(List<Coel> coels)
    {
        if (_units.Count > 0)
        {
            List<Unit> unitsToSend = _freeUnits.Where(unit => unit.State == UnitState.Free).Take(coels.Count).ToList();

            for (int i = 0; i < unitsToSend.Count; i++)
            {
                _freeUnits.Remove(unitsToSend[i]);
            }

            FreeUnitsChanged?.Invoke(_freeUnits.Count);

            _deliver.StartDelivering(coels, unitsToSend);
        }
    }

    private void OnCoelGet(Coel coel)
    {
        _coels.Add(coel);

        switch (_state)
        {
            case BaseState.Extension:

                if (_coels.Count >= _coelsToAddUnit)
                {
                    _coels.Clear();
                    Unit newUnit = NeedUnit?.Invoke();
                    AddUnit(newUnit);
                    newUnit.transform.position = _spawnPoint.position;

                    if (!_deliver.UnitsEnough)
                    {
                        _deliver.SendUnit(newUnit);
                    }
                    else
                    {
                        AddFreeUnit(newUnit);
                    }
                }

                break;

            case BaseState.Colonization:

                if (_coels.Count >= _coelsToColonize)
                {
                    _waitingToFirstFreeUnit = StartCoroutine(WaitFirstFreeUnit());
                }

                break;
        }

        CoelsChanged?.Invoke(_coels.Count);
    }

    private void AddFreeUnit(Unit unit)
    {
        if (!_freeUnits.Contains(unit))
        {
            _freeUnits.Add(unit);
            FreeUnitsChanged?.Invoke(_freeUnits.Count);
        }
    }

    private IEnumerator WaitFirstFreeUnit()
    {
        _deliver.OnNeedGetOnCome();

        if (_freeUnits.Count <= 0)
        {
            yield return new WaitUntil(() => _freeUnits.Count > 0);
        }

        _builderUnit = _freeUnits.First();
        _freeUnits.Remove(_builderUnit);
        GoingBuild?.Invoke(_builderUnit);
        _builderUnit.GoToBuild(_banner);
        _builderUnit.ReadyToBuild += OnReadyToBuild;

        _state = BaseState.Extension;
    }
}
