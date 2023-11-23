using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // allows custom class to be modified in inspector
public class Loot
{
    public Collectible thisLoot;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
    [SerializeField] int maxProb;

    public Collectible LootCollectible()
    {
        int cumulProb = 0;
        int currentProb = Random.Range(0, maxProb);
        Debug.Log("currentProb: " + currentProb);
        for (int i = 0; i < loots.Length; i++)
        {
            cumulProb += loots[i].lootChance;
            if (currentProb <= cumulProb)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }
}
