using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitTowardsMover _mover;
    [SerializeField] private UnitCollisionHandler _collisionHandler;
    [SerializeField] private Transform _basePlace;
    [SerializeField] private Taker _taker;

    public event Action<Unit> ComeToBase;
    public event Action<Coel> CoelCome;

    public UnitState State { get; private set; }
    private bool _haveCargo = false;

    private void OnEnable()
    {
        _collisionHandler.GettedCoel += OnCoelGet;
    }

    private void OnDisable()
    {
        _collisionHandler.GettedCoel -= OnCoelGet;
    }

    private void Start()
    {
        State = UnitState.Free;
    }

    private void OnCoelGet(Coel coel)
    {
        _taker.Take(coel.transform);
        _haveCargo = true;
        GoToBase();
    }
  
    public void GoToCoel(Coel coel)
    {
        Debug.Log(gameObject.name + " идет за рудой");
        State = UnitState.Delivering;
        _mover.MoveTo(coel.transform.position);
    }

    private void GoToBase()
    {
        Debug.Log("GoingBase " + transform.gameObject.name);
        _mover.MoveTo(_basePlace.position);
        _collisionHandler.ComeOnBase += OnComeToBase;
    }

    private void OnComeToBase()
    {
        if (_haveCargo)
        {
            if (_taker.Drop().TryGetComponent(out Coel coel))
            {
                CoelCome?.Invoke(coel);
                _haveCargo = false;
            }
        }

        _collisionHandler.ComeOnBase -= OnComeToBase;
        State = UnitState.Free;
        ComeToBase?.Invoke(this);
    }
}
