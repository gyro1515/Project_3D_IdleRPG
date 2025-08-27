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
    protected virtual void TakeDamage(float dam)
    {
        if (!baseCharacter) return;
        baseCharacter.Hp.CurrentValue -= dam;
        if (baseCharacter.Hp.CurrentValue == 0)
        {
            baseCharacter.IsDIe = true;
            OnDIe();
        }
    }
    protected abstract void OnDIe();
}
