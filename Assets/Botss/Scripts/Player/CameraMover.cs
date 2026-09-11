using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector2 _maxPosition;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _sprintSpeed;

    private float _resultSpeed;

    private void Awake()
    {
        _resultSpeed = _speed;
    }

    public void MoveX(float direction)
    {
        if (transform.position.x <= _maxPosition.x)
        {
            if (direction > 0)
            {
                transform.position += transform.right * _resultSpeed * Time.deltaTime;
            }
        }

        if (transform.position.x >= _minPosition.x)
        {
            if (direction < 0)
            {
                transform.position += -transform.right * _resultSpeed * Time.deltaTime;
            }
        }
    }

    public void MoveZ(float direction)
    {
        if (transform.position.z <= _maxPosition.y)
        {
            if (direction > 0)
            {
                transform.position += transform.forward * _resultSpeed * Time.deltaTime;
            }
        }

        if (transform.position.z >= _minPosition.y)
        {
            if (direction < 0)
            {
                transform.position += -transform.forward * _resultSpeed * Time.deltaTime;
            }
        }
    }

    public void OnPressSprint()
    {
        _resultSpeed = _sprintSpeed;
    }

    public void OnStopPressSprint()
    {
        _resultSpeed = _speed;
    }
}
