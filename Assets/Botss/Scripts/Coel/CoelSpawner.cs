using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoelSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private float _delay;
    [SerializeField] private CoelPool _pool;
    [SerializeField] private int _maxCoelsCount;

    private Coroutine _spawningCoroutine;
    private bool _isSpawning;

    private void Start()
    {
        _isSpawning = true;
        _spawningCoroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds time = new WaitForSeconds(_delay);

        while (_isSpawning)
        {
            if (!(_pool.Count >= _maxCoelsCount))
            {
                Coel coel = _pool.Get();
                coel.transform.position = GetPosition();
                coel.NeedToRelease += OnNeedToRelease;
            }

            yield return time;
        }
    }
    
    private void OnNeedToRelease(Coel coel)
    {
        coel.NeedToRelease -= OnNeedToRelease;
        _pool.Release(coel);
    }

    private Vector3 GetPosition()
    {
        return new Vector3(Random.Range(_minPosition.x, _maxPosition.x), transform.position.y, Random.Range(_minPosition.y, _maxPosition.y));
    }
}
