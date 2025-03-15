using UnityEngine;

public class Chest : MonoBehaviour, IAttackable
{
    [SerializeField] private ConfigReward configReward;
    private RewardDrop rewardDrop;
    public bool IsDead => false;
    public Transform Transform => transform;

    private void Start()
    {
        rewardDrop = GetComponent<RewardDrop>();
        rewardDrop.SetReward(configReward.RewardList);
    }
    public void TakeDamage(float damage)
    {
        //destroy chest when it takes any damage
        Die();
    }
    public void Die()
    {
        gameObject.SetActive(false);

        // drop item
        rewardDrop.DropReward();
    }
}
