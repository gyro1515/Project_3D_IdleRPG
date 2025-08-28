using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnTriggerEnter(Collider other)
    {
        // 앞인 경우만 데미지 적용
        Vector3 directionToOther = (other.gameObject.transform.position - player.gameObject.transform.position).normalized;
        float dotProduct = Vector3.Dot(player.transform.forward, directionToOther);
        if (dotProduct < 0f) return;
        // 오버라이드로 애너미만 처리하게 변경
        BaseController enemy = other.GetComponent<BaseController>();
        enemy?.TakeDamage(player.GetAttackDamage());


    }
}
