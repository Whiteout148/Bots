using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _reader;
    [SerializeField] private CameraMover _mover;
    [SerializeField] private Selector _selector;
    [SerializeField] private PlayerUI _playerUI;

    private void OnEnable()
    {
        _reader.MovingX += _mover.MoveX;
        _reader.MovingZ += _mover.MoveZ;
        _reader.PressSprint += _mover.OnPressSprint;
        _reader.StopPressSprint += _mover.OnStopPressSprint;
        _reader.pressRightMouse += _selector.Deselect;
        _reader.pressLeftMouse += _selector.OnClickLeft;
        _reader.pressRightMouse += _selector.OnClickRight;
        _selector.BaseSelected += _playerUI.OnBaseSelected;
        _selector.Deselected += _playerUI.OnDeselected;
    }

    private void OnDisable()
    {
        _reader.MovingX -= _mover.MoveX;
        _reader.MovingZ -= _mover.MoveZ;
        _reader.PressSprint -= _mover.OnPressSprint;
        _reader.StopPressSprint -= _mover.OnStopPressSprint;
        _reader.pressRightMouse -= _selector.Deselect;
        _reader.pressLeftMouse -= _selector.OnClickLeft;
        _reader.pressRightMouse -= _selector.OnClickRight;
        _selector.BaseSelected -= _playerUI.OnBaseSelected;
        _selector.Deselected -= _playerUI.OnDeselected;
    }
}
