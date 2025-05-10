using System.Collections.Generic;
using UnityEngine;

public class RarityCalculator
{
    private List<(Rarity rarity, float rate)> rarityList = new List<(Rarity, float)>()
    {
        (Rarity.Common, 100),
        (Rarity.Uncommon, 40),
        (Rarity.Rare, 10),
        (Rarity.Epic, 5),
        (Rarity.Lengendary, 1)
    };

    private float dropRatePerItem = 30;

    public Rarity GetNormalMonsterRarity()
    {
        float dropRate = Random.Range(0f, 100f);

        if(dropRate > dropRatePerItem) return Rarity.None;

        Rarity currentRarity = Rarity.Common;
        float totalRate = 0;

        foreach(var rarity in rarityList)
        {
            totalRate += rarity.rate;
        }

        float rollRate = Random.Range(0f, totalRate);
        float cumulative = 0f;

        foreach(var (rarity, rate) in rarityList)
        {
            cumulative += rate;

            if(rollRate < cumulative)
            {
                currentRarity = rarity;
                break;
            }
        }

        return currentRarity;
    }
}