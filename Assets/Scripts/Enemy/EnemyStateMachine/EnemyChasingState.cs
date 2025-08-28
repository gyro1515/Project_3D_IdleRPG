using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.Target || stateMachine.Target.IsDIe) // 쫒을 적이 없다면
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        if (IsInAttackRange()) // 사정거리 안이라면
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}
