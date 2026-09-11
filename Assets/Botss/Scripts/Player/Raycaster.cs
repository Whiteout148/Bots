using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _include;

    public event Action<Vector3> Hitted;
    public event Action<ISelectable> SelectableHitted;

    public void OnClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.gameObject.TryGetComponent(out ISelectable selecteable))
            {
                Debug.Log("hit");
                SelectableHitted?.Invoke(selecteable);
            }          
        }
    }

    public Vector3 GetHittedPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _include);
        Vector3 point = hit.point;

        return point;
    }
}
