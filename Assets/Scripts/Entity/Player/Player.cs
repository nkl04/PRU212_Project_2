using UnityEngine;
using ControlFreak2;

public class Player : MonoBehaviour
{

    public EntityInfo PlayerInfo { get => playerInfo; }
    private StateMachine<PlayerState> stateMachine;

    [Header("Data")]
    [SerializeField] private EntityInfo playerInfo;

    private void Start()
    {
        stateMachine = new StateMachine<PlayerState>();
        stateMachine.ChangeState(new PlayerStateIdle(this, stateMachine));
    }

    private void Update()
    {
        var moveX = CF2Input.GetAxis("Horizontal") * playerInfo._speed * Time.deltaTime;
        var moveY = CF2Input.GetAxis("Vertical") * playerInfo._speed * Time.deltaTime;
        var p = transform.position;
        p = new Vector3(p.x + moveX, p.y + moveY, p.z);
        transform.position = p;
    }
}
