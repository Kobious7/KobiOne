using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItemUISpawner : PrefabSpawner
{
    [SerializeField] private int rewardExp;
    [SerializeField] private int rewardPrimarionSoul;
    [SerializeField] private List<InventoryStuff> rewardItemList;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitNextFrame());
    }

    private IEnumerator WaitNextFrame()
    {
        yield return null;

        rewardExp = InfiniteMapManager.Instance.Player.StatsSystem.ExpFromBattle;
        rewardPrimarionSoul = InfiniteMapManager.Instance.Inventory.RewardPrimarionSoul;
        rewardItemList = InfiniteMapManager.Instance.Inventory.RewardItemList;

        StartCoroutine(SpawnRewards(rewardExp, rewardPrimarionSoul, rewardItemList));
    }

    private IEnumerator SpawnRewards(int exp, int primarionSoul, List<InventoryStuff> rewardItemList)
    {
        if (exp > 0)
        {
            Transform expReward = Spawn(prefabs[0], Vector3.zero, Quaternion.identity);
            expReward.localScale = Vector3.one;

            RewardItemUI rewardItemUICom = expReward.GetComponent<RewardItemUI>();

            rewardItemUICom.SetExpRewardItemUI(exp);

            expReward.gameObject.SetActive(true);

            exp = 0;
        }

        yield return new WaitForSeconds(0.5f);

        if (primarionSoul > 0)
        {
            Transform primaSoulReward = Spawn(prefabs[1], Vector3.zero, Quaternion.identity);
            primaSoulReward.localScale = Vector3.one;

            RewardItemUI rewardItemUICom = primaSoulReward.GetComponent<RewardItemUI>();

            rewardItemUICom.SetPrimaSoulRewardItemUI(primarionSoul);

            primaSoulReward.gameObject.SetActive(true);

            primarionSoul = 0;
        }

        if (rewardItemList.Count > 0)
        {
            foreach (InventoryStuff item in rewardItemList)
            {
                if (item is InventoryItem)
                {
                    yield return new WaitForSeconds(0.5f);

                    Transform itemReward = Spawn(prefabs[2], Vector3.zero, Quaternion.identity);
                    itemReward.localScale = Vector3.one;

                    RewardItemUI rewardItemUICom = itemReward.GetComponent<RewardItemUI>();

                    rewardItemUICom.SetRewardItemUI(item);

                    itemReward.gameObject.SetActive(true);
                }
            }

            foreach (InventoryStuff item in rewardItemList)
            {
                if (item is InventoryEquip)
                {
                    yield return new WaitForSeconds(0.5f);
                    
                    Transform equipReward = Spawn(prefabs[2], Vector3.zero, Quaternion.identity);
                    equipReward.localScale = Vector3.one;

                    RewardItemUI rewardItemUICom = equipReward.GetComponent<RewardItemUI>();

                    rewardItemUICom.SetRewardItemUI(item);

                    equipReward.gameObject.SetActive(true);
                }
            }

            rewardItemList = new();
        }
    }
}