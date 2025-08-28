using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    Player player;
    private PlayerStateMachine stateMachine;
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        stateMachine = new PlayerStateMachine(player);
    }
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    private void Update()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    public void AddExp(int amount)
    {
        if (!player) return;
        if(player.Exp.CurrentValue + amount >= player.Exp.MaxValue) // 레벨업
        {
            player.Level++;
            float overflow = (player.Exp.CurrentValue + amount) - player.Exp.MaxValue;
            player.Exp.CurrentValue = overflow;
            player.Exp.MaxValue *= 1.2f; // 최대 경험치 20% 증가
        }
        else
        {
            player.Exp.CurrentValue += amount;
        }
    }
    protected override void OnDIe()
    {
        base.OnDIe();
        // 죽으면 게임오버
    }
}
