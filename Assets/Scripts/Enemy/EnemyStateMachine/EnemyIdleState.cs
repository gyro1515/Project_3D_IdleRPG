using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.Target || stateMachine.Target.IsDIe) return; // 쫒을 적이 없다면 아무것도 안함

        if (IsInAttackRange()) // 사정거리 안
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }

    }
}
