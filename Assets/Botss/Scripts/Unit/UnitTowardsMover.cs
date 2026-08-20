using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitTowardsMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    public void MoveTo(Vector3 position)
    {
        _agent.isStopped = true;
        _agent.isStopped = false;
        _agent.SetDestination(position);
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
    }
}
