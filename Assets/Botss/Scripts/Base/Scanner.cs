using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const float RadiusFactor = 0.5f;

    [SerializeField] private float _speed;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _sphere;
    [SerializeField] private Vector3 _maxScale;

    private Coroutine _scanningCoroutine;
    private bool _isScanning = false;

    public event Action<List<Coel>> CoelsFounded;

    private void Start()
    {
        _isScanning = true;
        StartScan();
    }

    public void StartScan()
    {
        if (_scanningCoroutine == null)
        {
            _scanningCoroutine = StartCoroutine(Scan());
        }
    }

    public void OffScan()
    {
        if (_scanningCoroutine != null)
        {
            _isScanning = false;
            StopCoroutine(_scanningCoroutine);
            _scanningCoroutine = null;
        }
    }

    private IEnumerator Scan()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (_isScanning)
        {
            Vector3 normalScale = _sphere.localScale;

            while (_sphere.localScale.x < _maxScale.x)
            {
                _sphere.localScale += Vector3.one * _speed * Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            float radius = _sphere.lossyScale.x * RadiusFactor;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            HashSet<Coel> coels = new HashSet<Coel>();

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out Coel coel))
                {
                    coels.Add(coel);
                }
            }

            if (coels.Count > 0)
            {
                Debug.Log("Scan result " + coels.Count);
                CoelsFounded?.Invoke(coels.ToList());
            }

            _sphere.localScale = normalScale;

            yield return delay;
        }

        _scanningCoroutine = null;
    }
}
