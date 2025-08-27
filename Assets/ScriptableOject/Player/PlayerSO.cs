using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Characters/Player")]

public class PlayerSO : CharacterSO
{
    [field: Header("플레이어 스탯 세팅")]
    [field: SerializeField][field: Range(1, 1000)] public int MaxMp { get; private set; } = 200;
    [field: SerializeField][field: Range(1, 1000)] public int MaxExp { get; private set; } = 5;
}
