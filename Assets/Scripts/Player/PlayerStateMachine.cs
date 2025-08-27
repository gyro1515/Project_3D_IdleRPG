using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public bool IsAttacking { get; set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerChasingState ChasingState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerStateMachine(Player player)
    {
        this.Player = player;
        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        AttackState = new PlayerAttackState(this);
        MovementSpeed = player.PlayerData.GroundData.BaseSpeed;
        RotationDamping = player.PlayerData.GroundData.BaseRotationDamping;
    }
}
