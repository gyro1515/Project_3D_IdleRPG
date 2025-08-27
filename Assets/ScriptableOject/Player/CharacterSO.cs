using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GroundData
{
    [field: Header("무브 스피드")]
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 10f;
    [field: Header("회전 스피드")]
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 5f;
}

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Characters/Character")]
public class CharacterSO : ScriptableObject
{
    [field: Header("체력 스탯 세팅")]
    [field: SerializeField][field: Range(1, 1000)] public int MaxHp { get; private set; } = 200;
    // 자동으로 힙 할당해주지만, 명시적으로 사용하자.
    [field: Header("무브 수치 세팅")]
    [field: SerializeField] public GroundData GroundData { get; private set; } = new GroundData();
}
