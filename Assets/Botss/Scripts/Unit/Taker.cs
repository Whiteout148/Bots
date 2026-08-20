using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Taker : MonoBehaviour
{
    [SerializeField] private Transform _hand;

    private Transform _currentItem;

    public void Take(Transform item)
    {
        if (item.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
        }

        item.parent = _hand;
        item.localPosition = Vector3.zero;
        _currentItem = item;
    }

    public Transform Drop()
    {
        Transform item = _currentItem;

        if (_currentItem.gameObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = false;
        }

        _currentItem = null;

        return item;
    }
}
