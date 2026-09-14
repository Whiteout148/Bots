using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoelsRepository : MonoBehaviour
{
    private HashSet<Coel> _notedCoels = new HashSet<Coel>();

    public List<Coel> GetTargetCoels(List<Coel> coels)
    {
        List<Coel> targetCoelsToSet = new List<Coel>();

        for (int i = 0; i < coels.Count; i++)
        {
            if (!_notedCoels.Contains(coels[i]))
            {
                targetCoelsToSet.Add(coels[i]);
            }
        }

        _notedCoels.AddRange(targetCoelsToSet);
        return targetCoelsToSet;
    }

    public void RemoveOnNoteds(Coel coel)
    {
        if (_notedCoels.Contains(coel))
        {
            _notedCoels.Remove(coel);
        }
    }
}
