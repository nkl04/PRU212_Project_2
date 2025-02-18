using UnityEngine;
public class EnemyStateAttack : EnemyState
{
    private float nextAttackTime;
    public EnemyStateAttack(Enemy enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void CheckChangeState()
    {
        if (!enemy.Target)
        {
            stateMachine.ChangeState(enemy.EnemyStateIdle);
        }
        else if (enemy.CanFollowPlayer && !enemy.CanAttackPlayer)
        {
            stateMachine.ChangeState(enemy.EnemyStateRun);
        }
    }

    public override void Enter()
    {
        enemy.CanFollowPlayer = false;
        nextAttackTime = enemy.EntityInfo._baseAttackRate;
    }

    public override void Execute()
    {
        CheckChangeState();
        nextAttackTime += Time.deltaTime;

        if (nextAttackTime >= enemy.EntityInfo._baseAttackRate)
        {
            IAttackable target = enemy.Target.GetComponent<IAttackable>();
            if (target == null)
            {
                Debug.Log("Target is not attackable");
                return;
            }
            enemy.EnemyAttack.Attack(target, enemy.EntityInfo._baseDamage);
            nextAttackTime = 0;
        }
    }

    public override void Exit()
    {
    }
}
