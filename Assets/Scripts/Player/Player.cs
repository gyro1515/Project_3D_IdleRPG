using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [Header("플레이어 세팅")]
    [SerializeField] PlayerSO playerData;
    public PlayerSO PlayerData { get => playerData; }
    PlayerController playerController;
    public CharacterStat Mp { get; set; }
    public CharacterStat Exp { get; set; }
    int level = 1;
    // 보유 골드
    public int Gold { get; set; } = 0;
    public int Level { get => level;
        set
        {
            level = value;
            OnLevelUp?.Invoke(level);
        } 
    }
    public event Action<int> OnLevelUp;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
        Hp = new CharacterStat(playerData.MaxHp);
        Mp = new CharacterStat(playerData.MaxMp);
        Exp = new CharacterStat(playerData.MaxExp);
    }
    public void UIInit()
    {
        Hp.CurrentValue = Hp.CurrentValue;
        Mp.CurrentValue = Mp.CurrentValue;
        Exp.CurrentValue = 0;
        Level = 1;
    }
}
