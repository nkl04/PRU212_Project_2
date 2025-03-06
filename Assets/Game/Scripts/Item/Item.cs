using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour
{
    private bool followPlayer = false;
    private Transform player;
    [Header("Animation Variables")]
    [SerializeField] private float range = 2f;
    [SerializeField] public float speed = 5f;
    private float acceleration = 0.1f;
    private bool isTriggered = false;
    private Vector2 pushDirection;

    private void OnEnable()
    {
        followPlayer = false;
        isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            player = other.transform;
            pushDirection = (transform.position - player.position).normalized;
        }
    }

    private void Update()
    {
        if (player == null) return;

        if (!followPlayer && isTriggered)
        {
            speed -= acceleration * Time.deltaTime;
            transform.position += (Vector3)(pushDirection * speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) >= range)
            {
                followPlayer = true;
            }
        }
        else if (followPlayer)
        {
            speed += acceleration * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) < 0.1f)
            {
                Action(player.GetComponent<PlayerController>());
                DisableSelf();
            }
        }
    }

    protected abstract void Action(PlayerController playerController);

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
