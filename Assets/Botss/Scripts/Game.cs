using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private UnitPool _unitPool;
    [SerializeField] private BasePool _basePool;
    [SerializeField] private List<Base> _bases;

    private void OnEnable()
    {
        _bases.First().NeedUnit += _unitPool.Get;
        _bases.First().GoingBuild += _basePool.OnGoToBuild;
        _basePool.BaseBuild += OnBaseBuilded;
    }

    private void OnDisable()
    {
        _bases.First().NeedUnit -= _unitPool.Get;
        _bases.First().GoingBuild -= _basePool.OnGoToBuild;
        _basePool.BaseBuild -= OnBaseBuilded;
    }

    private void Awake()
    {
        _bases.First().AddUnit(_unitPool.Get());
        _bases.First().Init();
    }

    private void OnBaseBuilded(Base bass, Unit unit)
    { 
        bass.AddUnit(unit);
        bass.NeedUnit += _unitPool.Get;
        bass.GoingBuild += _basePool.OnGoToBuild;
        bass.Init();
        _bases.Add(bass);
    }
}
