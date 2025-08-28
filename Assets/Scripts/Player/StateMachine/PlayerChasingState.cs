using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChasingState : PlayerBaseState
{
    public PlayerChasingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!stateMachine.Target) // 쫒을 적이 없다면
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (IsInAttackRange()) // 사정거리 안이라면
        {
            if(stateMachine.Player.PlayerEquipment.CurEquipments[EEquipmentType.Weapon] == null)
            {
                stateMachine.ChangeState(stateMachine.IdleState); // 무기 없다면 대기 상태
                return;
            }
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    
}
