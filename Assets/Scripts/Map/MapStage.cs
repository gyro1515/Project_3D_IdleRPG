using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMapDiraction
{
    None,
    Up,
    Down,
    Left,
    Right,
}
public class MapStage : MonoBehaviour
{
    [Header("맵 세팅")]
    [SerializeField] List<EMapDiraction> road = new List<EMapDiraction> ();
    [SerializeField] int mapIdx = -1;
    public int MapIdx { get { return mapIdx; } }
    public bool IsActive { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if (IsActive) return;
        PlayerController controller = other.GetComponent<PlayerController>();
        controller.IsNavMesh = false;
        IsActive = true;
        // 몬스터 소환
        Debug.Log("스폰");
        GameManager.Instance.SpawnAndSettingEnemy(gameObject.transform.position);
        MapManager.Instance.ResetPreStage();
    }

}
