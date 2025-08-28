using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Enemy : BaseCharacter
{
    [Header("적 세팅")]
    [SerializeField] EnemySO enemyData;
    [SerializeField] Transform weaponSocket;

    EnemyController enemyController;
    public EnemySO EnemyData { get => enemyData; }
    protected override void Awake()
    {
        base.Awake();
        enemyController = GetComponent<EnemyController>();
        Hp = new CharacterStat(EnemyData.MaxHp);
        Instantiate(enemyData.WeaponDataSO.EquipPrefab, weaponSocket);
    }
    private void Start()
    {
        SetAttackRange();
        attackRangeCol.SetActive(false);
    }
    public int DropGold()
    {
        return UnityEngine.Random.Range(0, EnemyData.Gold) + 1;
    }
    public override float GetAttackRange()
    {
        return enemyData.WeaponDataSO.AttackRange * AttackRangeModifier;
    }

    public override float GetAttackDamage()
    {
        return enemyData.WeaponDataSO.Damage * AttackDamageModifier;
    }
}
