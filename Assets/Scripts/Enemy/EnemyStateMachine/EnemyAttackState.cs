using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    private bool alreadyAppliedDealing;

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.Animator, "Attack"); // 0~1만 반환하도록 설정
        if (normalizedTime < 0.95f) // 반복 재생이라 딱 1이 아니라 0.95f로 설정
        {
            // 추후 콜라이더 활성화 시간 무기에서 가져오도록 변경
            if (!alreadyAppliedDealing && normalizedTime >= 0.25f)
            {
                // 콜라이더 활성화
                alreadyAppliedDealing = true;
                stateMachine.Enemy.SetAttackColActive(true);
            }

            if (alreadyAppliedDealing && normalizedTime >= 0.4f)
            {
                // 콜라이더 비활성화
                stateMachine.Enemy.SetAttackColActive(false);
            }
        }
        else // 재생이 끝났다면
        {
            if (stateMachine.Target && !stateMachine.Target.IsDIe) // 쫒을 적이 있다면
            {
                if (IsInAttackRange())
                {
                    alreadyAppliedDealing = false;
                }
                else // 사정거리 밖이라면
                {
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
