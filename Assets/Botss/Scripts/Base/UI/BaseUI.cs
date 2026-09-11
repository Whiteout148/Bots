using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private BaseLineShower _lineShower;
    [SerializeField] private CountShower _freeUnitsShower;
    [SerializeField] private CountShower _unitsShower;
    [SerializeField] private CountShower _coelsShower;

    private void OnEnable()
    {
        _base.Selected += OnSelected;
        _base.Deselected += OnDeselected;
        _base.FreeUnitsChanged += _freeUnitsShower.OnValueChanged;
        _base.UnitsChanged += _unitsShower.OnValueChanged;
        _base.CoelsChanged += _coelsShower.OnValueChanged;
    }

    private void OnDisable()
    {
        _base.Selected -= OnSelected;
        _base.Deselected -= OnDeselected;
        _base.FreeUnitsChanged -= _freeUnitsShower.OnValueChanged;
        _base.UnitsChanged -= _unitsShower.OnValueChanged;
        _base.CoelsChanged -= _coelsShower.OnValueChanged;
    }

    private void Start()
    {
        OnDeselected();
    }

    private void OnSelected()
    {
        _lineShower.gameObject.SetActive(true);
        _freeUnitsShower.gameObject.SetActive(true);
        _unitsShower.gameObject.SetActive(true);
        _coelsShower.gameObject.SetActive(true);
    }

    private void OnDeselected()
    {
        _lineShower.gameObject.SetActive(false);
        _freeUnitsShower.gameObject.SetActive(false);
        _unitsShower.gameObject.SetActive(false);
        _coelsShower.gameObject.SetActive(false);
    }
}
