using UnityEngine;
using System.Collections;
using DG.Tweening;

public abstract class Item : MonoBehaviour
{
    private bool followPlayer = false;
    private Transform player;
    [Header("Animation Variables")]
    [SerializeField] public float pushForce = 5f;
    [SerializeField] public float followSpeed = 5f;
    [SerializeField] public float delayBeforeFollow = 0.5f;
    private bool isTriggered = false;

    private void OnEnable()
    {
        followPlayer = false;
        isTriggered = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isTriggered)
            {
                player = other.transform;
                transform.DOMove(transform.position + (transform.position - player.position).normalized * pushForce, delayBeforeFollow).SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        followPlayer = true;
                        isTriggered = true;
                    });
            }
            else
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    Action(playerController);
                    DisbleSelf();
                }
            }
        }
    }

    private void Update()
    {
        if (followPlayer && player != null)
        {
            transform.DOMove(player.position, followSpeed * Time.deltaTime).SetEase(Ease.InQuad);
        }
    }

    protected abstract void Action(PlayerController playerController);

    private void DisbleSelf()
    {
        gameObject.SetActive(false);
    }
}
