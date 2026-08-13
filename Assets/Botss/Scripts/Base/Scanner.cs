using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const float RadiusDivider = 0.5f;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _sphere;
    [SerializeField] private Vector3 _maxScale;

    private Coroutine _scanningCoroutine;

    public event Action<Coel> CoelFounded;

    private void Start()
    {
        StartScan();
    }

    public void StartScan()
    {
        if (_scanningCoroutine == null)
        {
            _scanningCoroutine = StartCoroutine(Scan());
        }
    }

    private IEnumerator Scan()
    {
        Vector3 normalScale = _sphere.localScale;

        while (_sphere.localScale.x < _maxScale.x)
        {
            _sphere.localScale += Vector3.one * _speed * Time.deltaTime;

            float radius = _sphere.localScale.x * 0.5f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log(colliders[i].gameObject.name);

                if (colliders[i].TryGetComponent(out Coel coel))
                {
                    CoelFounded?.Invoke(coel);
                }
            }

            yield return null;
        }

        _sphere.localScale = normalScale;
        _scanningCoroutine = null;
    }
}
