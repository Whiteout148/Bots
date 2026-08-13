using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouse : MonoBehaviour
{
    private List<Coel> _coels = new List<Coel>();

    public void AddCoel(Coel coel)
    {
        _coels.Add(coel);
    }
}
