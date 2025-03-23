using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardDrop : MonoBehaviour
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
                    if (reward.HasAnimation)
                    {
                        Drop(rewardObject);
                    }
                }
            }
        }
    }

    public void Drop(GameObject gameObject)
    {
        Vector2[] directions = new Vector2[]
        {
        new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
        new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(-1, -1),
        new Vector2(0, -1), new Vector2(1, -1),
        };

        Vector2 direction = directions[Random.Range(0, directions.Length)];
        float moveDistance = Random.Range(1f, 2f);
        Vector3 startPos = transform.position;
        Vector3 peakPos = startPos + new Vector3(0, 0.5f, 0); // Nhảy lên
        Vector3 targetPos = startPos + (Vector3)direction * moveDistance; // Rơi xuống

        // Tạo quỹ đạo cong
        Vector3[] path = new Vector3[] { startPos, peakPos, targetPos };

        gameObject.transform.DOPath(path, 0.6f, PathType.CatmullRom)
            .SetEase(Ease.OutQuad);
    }

}
