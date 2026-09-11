using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BannerSetter : MonoBehaviour
{
    [SerializeField] private Banner _previewBanner;
    [SerializeField] private Material _previewMaterial;
    [SerializeField] private float _circleRadius;

    private Banner _currentBanner;
    private Coroutine _settingCoroutine;
    private bool _isSetting = false;
    private bool _settalbe;

    public event Action Setted;
    public Func<Vector3> ToGetPosition;

    public void OnBannerGet(Banner banner)
    {
        _currentBanner = null;
        _currentBanner = banner;
    }

    public void StartSet()
    {
        if (!_isSetting && _settingCoroutine == null)
        {
            _isSetting = true;
            _previewBanner.gameObject.SetActive(true);

            _settingCoroutine = StartCoroutine(SetPosition());
        }
    }

    private IEnumerator SetPosition()
    {
        Vector3 beforePosition = Vector3.zero;
        bool canSet;

        _settalbe = false;

        while (_isSetting)
        {
            canSet = true;

            _previewBanner.transform.position = ToGetPosition();

            Collider[] hits = Physics.OverlapSphere(_previewBanner.transform.position, _circleRadius);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent<ICantCoelSpawnable>(out _))
                {
                    canSet = false;
                }
            }

            _settalbe = canSet;

            if (_settalbe)
            {
                _previewMaterial.color = Color.white;
            }
            else
            {
                _previewMaterial.color = Color.red;
            }

            yield return null;
        }
    }

    public void CancelSet()
    {
        if (_isSetting && _settingCoroutine != null)
        {
            _isSetting = false;
            StopCoroutine(_settingCoroutine);
            _settingCoroutine = null;
            _previewBanner.gameObject.SetActive(false);
        }
    }

    public void Set()
    {
        if (_isSetting && _settingCoroutine != null && _settalbe)
        {
            _isSetting = false;
            StopCoroutine(_settingCoroutine);
            _settingCoroutine = null;
            _currentBanner.gameObject.SetActive(true);
            _currentBanner.transform.parent = null;
            _currentBanner.transform.position = _previewBanner.transform.position;
            Setted?.Invoke();
            _previewBanner.gameObject.SetActive(false);
        }
    }
}
