using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitTowardsMover _mover;
    [SerializeField] private UnitCollisionHandler _collisionHandler;
    [SerializeField] private Transform _basePlace;
    [SerializeField] private Taker _taker;

    public event Action<Unit, Coel> ComeToBase;
    public event Action<Vector3, Unit> ReadyToBuild;

    public UnitState State { get; private set; }
    private bool _haveCargo = false;
    private Banner _currentBanner;

    private void OnEnable()
    {
        _collisionHandler.GettedCoel += OnCoelGet;
        _collisionHandler.BannerTouched += OnBannerTouched;
    }

    private void OnDisable()
    {
        _collisionHandler.GettedCoel -= OnCoelGet;
        _collisionHandler.BannerTouched -= OnBannerTouched;
    }

    private void Start()
    {
        State = UnitState.Free;
    }

    public void Init(Transform basePlace)
    {
        _basePlace = basePlace;
        _mover.MoveTo(basePlace.position);
    }

    public void GoToBuild(Banner banner)
    {
        State = UnitState.Building;
        _mover.MoveTo(banner.transform.position);
        _currentBanner = banner;
    }

    public void GoToCoel(Coel coel)
    {
        State = UnitState.Delivering;
        _mover.MoveTo(coel.transform.position);
    }

    private void OnBannerTouched(Banner banner)
    {
        if (_currentBanner == banner)
        {
            ReadyToBuild?.Invoke(banner.BuildingPosition, this);
            State = UnitState.Free;
        }
    }

    private void OnCoelGet(Coel coel)
    {
        _taker.Take(coel.transform);
        _haveCargo = true;
        GoToBase();
    }

    private void GoToBase()
    {
        _mover.MoveTo(_basePlace.position);
        _collisionHandler.ComeOnBase -= OnComeToBase;
        _collisionHandler.ComeOnBase += OnComeToBase;
    }

    private void OnComeToBase()
    {
        _collisionHandler.ComeOnBase -= OnComeToBase;
        State = UnitState.Free;

        if (_haveCargo)
        {
            if (_taker.Drop().TryGetComponent(out Coel coel))
            {
                _haveCargo = false;

                ComeToBase?.Invoke(this, coel);
            }
        }
        else
        {
            ComeToBase?.Invoke(this, null);
        }
    }
}
