using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    protected List<T> InactiveItems = new List<T>();
    protected List<T> ActiveItems = new List<T>();

    [SerializeField] protected T Prefab;

    public event Action<IReadOnlyList<T>> ReleasedAll;

    public void Init(T prefab, int baseCount = 10)
    {
        Prefab = prefab;

        for (int i = 0; i < baseCount; i++)
        {
            InactiveItems.Add(Create());
        }
    }

    public virtual T Get()
    {
        T itemToGet;

        if (InactiveItems.Count <= 0)
        {
            InactiveItems.Add(Create());
        }

        itemToGet = InactiveItems[0];
        InactiveItems.RemoveAt(0);
        ActiveItems.Add(itemToGet);

        return itemToGet;
    }

    public virtual void Release(T itemToRelease)
    {
        InactiveItems.Add(itemToRelease);
        ActiveItems.RemoveAt(SearchIndex(itemToRelease, ActiveItems));
    }

    public virtual void ReleaseAll()
    {
        ReleasedAll?.Invoke(ActiveItems);
    }

    protected abstract T Create();

    private int SearchIndex(T item, List<T> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (item == items[i])
            {
                return i;
            }
        }

        return -1;
    }
}