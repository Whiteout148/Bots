using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLineShower : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    private void Awake()
    {
        OnDeselect();
    }

    public void OnSelect()
    {
        _renderer.enabled = true;
    }

    public void OnDeselect()
    {
        _renderer.enabled = false;
    }
}
