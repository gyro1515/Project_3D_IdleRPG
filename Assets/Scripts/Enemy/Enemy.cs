using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    [Header("적 세팅")]
    [SerializeField] EnemySO EnemyData;
    EnemyController enemyController;
    protected override void Awake()
    {
        base.Awake();
        enemyController = GetComponent<EnemyController>();
        Hp = new CharacterStat(EnemyData.MaxHp);
    }
    public int DropGold()
    {
        return UnityEngine.Random.Range(0, EnemyData.Gold) + 1;
    }
}
