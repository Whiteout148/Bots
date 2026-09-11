using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _baseSelectionInfo;

    public void OnBaseSelected()
    {
        _baseSelectionInfo.alpha = 1;
        _baseSelectionInfo.interactable = true;
    }

    public void OnDeselected()
    {
        _baseSelectionInfo.alpha = 0;
        _baseSelectionInfo.interactable = false;
    }
}
