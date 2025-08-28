using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        // 앞인 경우만 데미지 적용
        Vector3 directionToOther = (other.gameObject.transform.position - enemy.gameObject.transform.position).normalized;
        float dotProduct = Vector3.Dot(enemy.transform.forward, directionToOther);
        if (dotProduct < 0f) return;
        // 오버라이드로 플레이어만 처리하게 변경
        BaseController player = other.GetComponent<BaseController>();
        player?.TakeDamage(enemy.GetAttackDamage());
        Debug.Log("Player Hit! " + enemy.GetAttackDamage());

    }
}
