using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.Target) return; // 쫒을 적이 없다면 아무것도 안함

        if (IsInAttackRange() && stateMachine.Player.PlayerEquipment.CurEquipments[EEquipmentType.Weapon]) // 사정거리 안이고 무기가 있다면
        {
            Debug.Log("Idle -> Attack");
            stateMachine.ChangeState(stateMachine.AttackState);
        }
        else if(!IsInAttackRange()) // 사정거리 밖이면
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }

    }
}
