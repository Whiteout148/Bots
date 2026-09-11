using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour
{
    [SerializeField] private Transform _buildingTransform;

    public Vector3 BuildingPosition => _buildingTransform.position;
}
