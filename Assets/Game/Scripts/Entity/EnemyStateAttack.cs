using UnityEngine;
public class EnemyStateAttack : EnemyState
{
    private float nextAttackTime;
    public EnemyStateAttack(Enemy_Base enemy, StateMachine<EnemyState> stateMachine) : base(enemy, stateMachine)
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
        nextAttackTime = enemy.EnemyInfo._baseAttackRate;
    }

    public override void Execute()
    {
        CheckChangeState();
        nextAttackTime += Time.deltaTime;

        if (nextAttackTime >= enemy.EnemyInfo._baseAttackRate)
        {
            IAttackable target = enemy.Target.GetComponent<IAttackable>();
            if (target == null)
            {
                Debug.Log("Target is not attackable");
                return;
            }
            enemy.EnemyAttack.Attack(target, enemy.EnemyInfo._baseDamage);
            nextAttackTime = 0;
        }
    }

    public override void Exit()
    {
    }
}
