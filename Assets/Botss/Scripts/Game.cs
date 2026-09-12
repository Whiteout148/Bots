using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Game : MonoBehaviour
{
    [SerializeField] private UnitPool _unitPool;
    [SerializeField] private BasePool _basePool;
    [SerializeField] private List<Base> _bases;
    [SerializeField] private CoelsRepository _repository;

    private void OnEnable()
    {
        _bases.First().NeedUnit += _unitPool.Get;
        _bases.First().GoingBuild += _basePool.OnGoToBuild;
        _bases.First().CoelsGettedInScan += _repository.OnGetCoelsInScan;
        _bases.First().NeedRemoveOnNoteds += _repository.OnNeedRemoveOnNotes;
        _bases.First().CoelsNoted += _repository.OnNeedToNote;
        _basePool.BaseBuild += OnBaseBuilded;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _bases.Count; i++)
        {
            _bases.First().NeedUnit -= _unitPool.Get;
            _bases.First().GoingBuild -= _basePool.OnGoToBuild;
            _bases.First().CoelsGettedInScan -= _repository.OnGetCoelsInScan;
            _bases.First().NeedRemoveOnNoteds -= _repository.OnNeedRemoveOnNotes;
            _bases.First().CoelsNoted -= _repository.OnNeedToNote;
        }

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
        bass.CoelsGettedInScan += _repository.OnGetCoelsInScan;
        bass.NeedRemoveOnNoteds += _repository.OnNeedRemoveOnNotes;
        bass.CoelsNoted += _repository.OnNeedToNote;
        bass.NeedUnit += _unitPool.Get;
        bass.GoingBuild += _basePool.OnGoToBuild;
        bass.Init();
        _bases.Add(bass);
    }
}
