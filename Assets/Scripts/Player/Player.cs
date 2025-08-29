using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [Header("플레이어 세팅")]
    [SerializeField] PlayerSO playerData;
    public PlayerSO PlayerData { get => playerData; }
    public PlayerController PlayerController { get; private set; }
    public PlayerInventory PlayerInventory {  get; private set; }
    public PlayerEquipment PlayerEquipment {  get; private set; }
    public CharacterStat Mp { get; set; }
    public CharacterStat Exp { get; set; }
    int level = 1;
    // 보유 골드
    int gold = 999999;
    public event Action<int> OnGoldChanged;
    public int Gold { get { return gold; }
        set
        {
            gold = value;
            OnGoldChanged?.Invoke(gold);
        }
    }
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
        PlayerController = GetComponent<PlayerController>();
        PlayerInventory = GetComponent<PlayerInventory>();
        PlayerEquipment = GetComponent<PlayerEquipment>();
        Hp = new CharacterStat(playerData.MaxHp);
        Mp = new CharacterStat(playerData.MaxMp);
        Exp = new CharacterStat(playerData.MaxExp);
        
    }
    private void Start()
    {
        SetAttackRange();
        attackRangeCol.SetActive(false);
    }
    public void UIInit()
    {
        // UI 초기화
        Hp.CurrentValue = Hp.CurrentValue;
        Mp.CurrentValue = Mp.CurrentValue;
        Exp.CurrentValue = 0;
        Level = level;
        Gold = gold;
        
    }
    public override float GetAttackRange()
    {
        if (PlayerEquipment.CurEquipments[EEquipmentType.Weapon] == null) return 2; // 무기가 없으면 기본 2
        return PlayerEquipment.CurEquipments[EEquipmentType.Weapon].AttackRange * AttackRangeModifier; // 무기 공격 범위,
    }
    public override float GetAttackDamage()
    {
        if (PlayerEquipment.CurEquipments[EEquipmentType.Weapon] == null) return 0; // 무기가 없으면 기본 0
  
        return PlayerEquipment.CurEquipments[EEquipmentType.Weapon].Damage * AttackDamageModifier;
    }
    public void AddExp(int amount)
    {
        // 레벨 업 상황이라면
        if(Exp.CurrentValue + amount >= Exp.MaxValue)
        {
            int difference = Mathf.RoundToInt(Exp.CurrentValue + amount - Exp.MaxValue);
            Exp.AddMaxValue(Exp.MaxValue); // 2배씩 경험치 통 증가
            Level++;
            Exp.CurrentValue = difference;
            return;
        }
        // 레벨 업 아니면 경험치만 증가
        Exp.CurrentValue += amount;
    }    

}
