using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonMono<MapManager>
{
    [SerializeField] List<MapStage> mapStagesList = new List<MapStage>();
    List<Dictionary<EMapDiraction, int>> mapList = new List<Dictionary<EMapDiraction, int>>();

    public MapStage CurStage { get; set; }
    public MapStage PreStage { get; set; }
    public List<Dictionary<EMapDiraction, int>> MapList { get { return mapList; }}
    protected override void Awake()
    {
        base.Awake();
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[0].Add(EMapDiraction.Left, 1);
        mapList[0].Add(EMapDiraction.Down, 4);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[1].Add(EMapDiraction.Up, 7);
        mapList[1].Add(EMapDiraction.Down, 8);
        mapList[1].Add(EMapDiraction.Right, 0);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[2].Add(EMapDiraction.Up, 5);
        mapList[2].Add(EMapDiraction.Down, 6);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[3].Add(EMapDiraction.Left, 7);
        mapList[3].Add(EMapDiraction.Right, 5);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[4].Add(EMapDiraction.Up, 0);
        mapList[4].Add(EMapDiraction.Right, 6);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[5].Add(EMapDiraction.Left, 3);
        mapList[5].Add(EMapDiraction.Down, 2);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[6].Add(EMapDiraction.Up, 2);
        mapList[6].Add(EMapDiraction.Left, 4);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[7].Add(EMapDiraction.Right, 3);
        mapList[7].Add(EMapDiraction.Down, 1);
        mapList.Add(new Dictionary<EMapDiraction, int>());
        mapList[8].Add(EMapDiraction.Up, 1);

        CurStage = mapStagesList[0];
        CurStage.IsActive = true;
    }
    private void Start()
    {
        UIManager.Instance.SetStageText(CurStage.MapIdx);
    }
    public Vector3 GetNextStagePos()
    {
        return Vector3.zero;
    }
    public void MoveToNextStage(EMapDiraction dir)
    {
        int curIdx = CurStage.MapIdx;
        int nextIdx = mapList[curIdx][dir];
        PreStage = CurStage;
        CurStage = mapStagesList[nextIdx];
        UIManager.Instance.SetStageText(CurStage.MapIdx);
        GameManager.Instance.Player.PlayerController.MoveToPos(mapStagesList[nextIdx].gameObject.transform.position);
    }
    public void ResetPreStage()
    {
        if (!PreStage) return;
        PreStage.IsActive = false;
    }
}
