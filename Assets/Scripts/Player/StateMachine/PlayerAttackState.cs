using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool alreadyAppliedDealing;

    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            // 추후 콜라이더 활성화 시간 무기에서 가져오도록 변경
            if (!alreadyAppliedDealing && normalizedTime >= 0.25f)
            {
                // 콜라이더 활성화
                //Debug.Log($"공격 활성: {normalizedTime}");
                alreadyAppliedDealing = true;
            }

            if (alreadyAppliedDealing && normalizedTime >= 0.4f)
            {
                // 콜라이더 비활성화
                //Debug.Log($"공격 비활성: {normalizedTime}");
            }
        }
        else // 재생이 끝났다면
        {
            if (stateMachine.Target) // 쫒을 적이 있다면
            {
                if (IsInAttackRange())
                {
                    alreadyAppliedDealing = false;
                }
                else // 사정거리 밖이라면
                {
                    stateMachine.Target = null; // 일단 타겟 초기화
                    stateMachine.Target = GameManager.Instance.GetEnemyTarget(); // 타겟 갱신
                    if (IsInAttackRange()) // 갱신한 타겟이 사정거리 안이라면
                    {
                        alreadyAppliedDealing = false;
                    }
                    else // 갱신한 타겟도 사정거리 밖이라면
                    stateMachine.ChangeState(stateMachine.ChasingState);
                }
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }

    }
}
