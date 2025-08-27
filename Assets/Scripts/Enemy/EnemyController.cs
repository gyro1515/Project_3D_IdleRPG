using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    Enemy enemy;
    public event Action OnDropGold;
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }

    protected override void OnDIe()
    {
        if (!enemy) return;
        // 죽으면 골드 드랍
        StartCoroutine(DropGold(enemy.DropGold()));
    }
    IEnumerator DropGold(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // 골드 드랍 = 화면에 골드 이미지 생성하고 골드 UI로 날아가기
            OnDropGold?.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
