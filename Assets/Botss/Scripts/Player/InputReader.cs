using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private KeyCode _sprintKey;

    public event Action<float> MovingX;
    public event Action<float> MovingZ;
    public event Action PressSprint;
    public event Action StopPressSprint;

    private void Update()
    {
        float directionX = Input.GetAxis(Horizontal);
        float directionZ = Input.GetAxis(Vertical);

        

        if (!Mathf.Approximately(directionZ, 0))
        {
            MovingZ?.Invoke(directionZ);
        }

        if (Input.GetKeyDown(_sprintKey))
        {
            PressSprint?.Invoke();
        }

        if (Input.GetKeyUp(_sprintKey))
        {
            StopPressSprint?.Invoke();
        }

        if (!Mathf.Approximately(directionX, 0))
        {
            MovingX?.Invoke(directionX);
        }
    }
}
