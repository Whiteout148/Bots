using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoelsRepository : MonoBehaviour
{
    [SerializeField] private HashSet<Coel> _notedCoels = new HashSet<Coel>();

    public void OnGetCoelsInScan(List<Coel> coels, Base bass)
    {
        List<Coel> targetCoelsToSet = new List<Coel>();

        for (int i = 0; i < coels.Count; i++)
        {
            if (!_notedCoels.Contains(coels[i]))
            {
                targetCoelsToSet.Add(coels[i]);
            }
        }

        bass.OnTargetCoelsGet(targetCoelsToSet);
    }

    public void OnNeedToNote(List<Coel> coels)
    {
        _notedCoels.AddRange(coels);
    }

    public void OnNeedRemoveOnNotes(Coel coel)
    {
        _notedCoels.Remove(coel);
    }
}
