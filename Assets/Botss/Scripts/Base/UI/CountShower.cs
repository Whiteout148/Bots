using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _stroke;

    public void OnValueChanged(int value)
    {
        _text.text = _stroke + " " + value;
    }
}
