using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRewardDrop : MonoBehaviour
{
    private List<Reward> rewardList;

    public void SetReward(List<Reward> rewardList)
    {
        this.rewardList = rewardList;
    }

    public void DropReward()
    {
        if (rewardList == null || rewardList.Count <= 0) return;

        foreach (var reward in rewardList)
        {
            if (Random.Range(0, 100) <= reward.DropRate)
            {
                for (int i = 0; i < reward.Amount; i++)
                {
                    GameObject rewardObject = ObjectPooler.Instance.GetObjectFromPool(reward.RewardPrefab.name);
                    rewardObject.transform.position = transform.position;
                    rewardObject.SetActive(true);
                }
            }
        }
    }

}
