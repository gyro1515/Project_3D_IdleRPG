using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [Header("캐릭터 세팅")]
    [SerializeField] protected GameObject attackRangeCol;

    public CharacterStat Hp { get; set; }
    public Animator Animator { get; protected set; }
    public bool IsDIe { get; set; } = false;
    public CharacterController Controller { get; protected set; }
    [field: Header("Animations")]
    [field: SerializeField] public AnimationData AnimationData { get; private set; } = new AnimationData();
    public float AttackRangeModifier { get; set; } = 1f; // 공격 범위 보정값 (아이템으로 변경 가능하게)
    public float AttackDamageModifier { get; set; } = 1f; // 공격 데미지 보정값 (아이템으로 변경 가능하게)

    protected virtual void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
    }
    public void SetAttackColActive(bool active)
    {
        attackRangeCol.SetActive(active);
    }
    public void SetAttackRange()
    {
        float tmpRange = GetAttackRange();
        attackRangeCol.transform.localScale = new Vector3(tmpRange, tmpRange, tmpRange); // 공격 범위 콜라이더 크기 설정
    }
    public abstract float GetAttackRange();
    public abstract float GetAttackDamage();
}
