using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBars : BaseUI
{
    [SerializeField] GameObject enemyHpBarPrefab;

    List<EnemyHaBarImage> enemyHpBarImages = new List<EnemyHaBarImage>(); // 오브젝트 풀링

    public void AddEnemyHpBar(Enemy enemy)
    {
        for(int i = 0; i < enemyHpBarImages.Count; i++)
        {
            if (enemyHpBarImages[i].gameObject.activeSelf) continue; // 활성화된 상태면 패스

            enemyHpBarImages[i].Init(enemy);
            return; // 재사용하고 종료
        }
        // 재사용할 오브젝트가 없으면 새로 생성
        GameObject go = Instantiate(enemyHpBarPrefab, transform);
        EnemyHaBarImage enemyHaBarImage = go.GetComponent<EnemyHaBarImage>();
        if (enemyHaBarImage)
        {
            enemyHaBarImage?.Init(enemy);
            enemyHpBarImages.Add(enemyHaBarImage);
        }
    }
}
