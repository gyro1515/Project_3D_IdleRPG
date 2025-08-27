using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public CharacterStat Hp { get; set; }
    public Animator Animator { get; protected set; }
    public bool IsDIe { get; set; } = false;
    public CharacterController Controller { get; protected set; }
    [field: Header("Animations")]
    [field: SerializeField] public AnimationData AnimationData { get; private set; } = new AnimationData();
    protected virtual void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
    }
}
