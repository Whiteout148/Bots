using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private BaseLineShower _shower;
    [SerializeField] private CountShower _freeUnitsShower;
    [SerializeField] private CountShower _unitsShower;
    [SerializeField] private CountShower _coelsShower;

    private void OnEnable()
    {
        _base.Selected += _shower.OnSelect;
        _base.Deselected += _shower.OnDeselect;
        _base.FreeUnitsChanged += _freeUnitsShower.OnValueChanged;
        _base.UnitsChanged += _unitsShower.OnValueChanged;
        _base.CoelsChanged += _coelsShower.OnValueChanged;
    }

    private void OnDisable()
    {
        _base.Selected -= _shower.OnSelect;
        _base.Deselected -= _shower.OnDeselect;
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
        _freeUnitsShower.gameObject.SetActive(true);
        _unitsShower.gameObject.SetActive(true);
        _coelsShower.gameObject.SetActive(true);
    }

    private void OnDeselected()
    {
        _freeUnitsShower.gameObject.SetActive(false);
        _unitsShower.gameObject.SetActive(false);
        _coelsShower.gameObject.SetActive(false);
    }
}
