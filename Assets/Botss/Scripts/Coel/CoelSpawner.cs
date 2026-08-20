using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CoelSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private float _delay;
    [SerializeField] private float _hitOverlapRadius;
    [SerializeField] private CoelPool _pool;
    [SerializeField] private int _maxCoelsCount;
    [SerializeField] private float _positionYFactor;

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
                bool canSpawn = false;
                Vector3 position = Vector3.zero;

                while (!canSpawn)
                {
                    canSpawn = TryGetSpawn(out position);
                }

                if (canSpawn)
                {
                    Coel coel = _pool.Get();
                    Debug.Log("Coel spawned");

                    if (coel.TryGetComponent(out Rigidbody rigidbody))
                    {
                        rigidbody.isKinematic = false;
                    }

                    transform.parent = null;
                    Vector3 positionToSpawn = position;
                    coel.transform.position = positionToSpawn;
                    coel.NeedToRelease += OnNeedToRelease;
                }
            }

            yield return time;
        }
    }
    
    private bool TryGetSpawn(out Vector3 position)
    {
        Vector3 randomPosition = GetPosition();

        if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit))
        {
            if (!hit.transform.gameObject.TryGetComponent<ICantCoelSpawnable>(out _))
            {
                Collider[] hittedItems = Physics.OverlapSphere(hit.transform.position, _hitOverlapRadius);

                for (int i = 0; i < hittedItems.Length; i++)
                {
                    if (hittedItems[i].TryGetComponent<ICantCoelSpawnable>(out _))
                    {
                        position = Vector3.zero;
                        return false;
                    }
                }
            }
        }

        position = new Vector3(hit.point.x, hit.point.y + _positionYFactor, hit.point.z);
        return true;
    }

    private void OnNeedToRelease(Coel coel)
    {
        _pool.Release(coel);
        coel.NeedToRelease -= OnNeedToRelease;
    }

    private Vector3 GetPosition()
    {
        return new Vector3(Random.Range(_minPosition.x, _maxPosition.x), transform.position.y, Random.Range(_minPosition.y, _maxPosition.y));
    }
}
