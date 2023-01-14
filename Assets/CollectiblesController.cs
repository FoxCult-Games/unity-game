using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    public static CollectiblesController Instance;

    private readonly Dictionary<CollectiblesTypes, int> collected = new Dictionary<CollectiblesTypes, int>();

    private void Awake()
    {
        if(!Instance) Instance = this;
    }

    public int Collect(CollectiblesTypes collectible, int amount)
    {
        if (collected.ContainsKey(collectible))
        {
            collected[collectible] += amount;
            return collected[collectible];
        }

        collected.Add(collectible, amount);
        return amount;
    }

    public int GetCollected(CollectiblesTypes type)
    {
        return collected[type];
    }
}
