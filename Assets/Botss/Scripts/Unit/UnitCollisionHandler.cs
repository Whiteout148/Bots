using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCollisionHandler : CollisionHandler
{
    public event Action ComeOnBase;
    public event Action<Banner> BannerTouched;

    private bool _isInBase;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.TryGetComponent<Base>(out _))
        {
            if (!_isInBase)
            {
                Debug.Log("base cum");
                ComeOnBase?.Invoke();
                _isInBase = true;
            }
        }
        else if (other.gameObject.TryGetComponent(out Banner banner))
        {
            BannerTouched?.Invoke(banner);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Base>(out _))
        {
            _isInBase = false; 
        }
    }
}
