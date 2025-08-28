using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    BaseCharacter baseCharacter;
    protected virtual void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
    }
    public virtual void TakeDamage(float dam)
    {
        if (!baseCharacter) return;
        if (baseCharacter.IsDIe) return;
        baseCharacter.Hp.CurrentValue -= dam;
        //Debug.Log($"{baseCharacter.name}이 {dam}의 피해를 입었습니다. 남은 체력: {baseCharacter.Hp.CurrentValue}/{baseCharacter.Hp.MaxValue}");
        if (baseCharacter.Hp.CurrentValue == 0)
        {
            baseCharacter.IsDIe = true;
            OnDIe();
        }
    }
    protected virtual void OnDIe()
    {
        baseCharacter.IsDIe = true;
        baseCharacter.Animator.SetTrigger("Die");
        baseCharacter.Controller.excludeLayers = LayerMask.GetMask("Player", "Enemy");
        baseCharacter.SetAttackColActive(false);
    }
}
