using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    [SerializeField] private BannerSetter _bannerSetter;
    [SerializeField] private Raycaster _raycaster;

    private ISelectable _currentSelectable;

    public event Action BaseSelected;
    public event Action Deselected;

    private void OnEnable()
    {
        _raycaster.SelectableHitted += Select;
        _bannerSetter.ToGetPosition += _raycaster.GetHittedPosition;
    }

    private void OnDisable()
    {
        _raycaster.SelectableHitted -= Select;
        _bannerSetter.ToGetPosition -= _raycaster.GetHittedPosition;
    }

    public void OnClickLeft()
    {
        _raycaster.OnClicked();
        _bannerSetter.Set();
    }

    public void OnClickRight()
    {
        Deselect();
        _bannerSetter.CancelSet();   
    }

    public void Select(ISelectable selectable)
    {
        Deselect();

        if (selectable is Base)
        {
            Base bass = (Base)selectable;
            bass.BannerSended += _bannerSetter.OnBannerGet;
            _bannerSetter.Setted += bass.OnBannerSet;
            bass.OnSelect();
            _currentSelectable = bass;

            BaseSelected?.Invoke();
        }
    }

    public void Deselect()
    {
        if (_currentSelectable is Base)
        {
            Base bass = (Base)_currentSelectable;
            bass.BannerSended -= _bannerSetter.OnBannerGet;
            _bannerSetter.Setted -= bass.OnBannerSet;
            bass.OnDeselect();
        }

        _currentSelectable = null;
        Deselected?.Invoke();
    }
}
