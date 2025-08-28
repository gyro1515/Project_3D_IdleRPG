using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHaBarImage : MonoBehaviour
{
    [SerializeField] Image hpBarImg;
    Enemy enemy;

    void Update()
    {
        if (!enemy) return;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.gameObject.transform.position + Vector3.up * 2f);
        transform.position = screenPos;
    }
    public void Init(Enemy _enemy)
    {
        enemy = _enemy;
        enabled = true;
        enemy.Hp.OnValueChange += SetHpBar;
    }

    public void SetHpBar(float curHp, float maxHp)
    {
        if (!hpBarImg) return;
        float ratio = curHp / maxHp;
        hpBarImg.fillAmount = ratio;
        if (ratio <= 0f)
        {
            gameObject.SetActive(false);
            enemy.Hp.OnValueChange -= SetHpBar;
            enemy = null;
            enabled = false; // Update 비활성화
        }
    }
}
