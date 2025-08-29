using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : BaseUI
{
    [Header("스테이지 선택 세팅")]
    [SerializeField] Button returnlBtn;
    [SerializeField] Button upBtn;
    [SerializeField] Button downBtn;
    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;

    private void Awake()
    {
        returnlBtn?.onClick.AddListener(CloseUI);
        upBtn?.onClick.AddListener(MovetoUp);
        downBtn?.onClick.AddListener(MovetoDown);
        leftBtn?.onClick.AddListener(MovetoLeft);
        rightBtn?.onClick.AddListener(MovetoRight);

    }
    private void Start()
    {
        CloseUI();
    }
    public override void OpenUI()
    {
        base.OpenUI();
        returnlBtn?.gameObject.SetActive(true);

        // 현재 방과 연결된 방 방향의 버튼 활성화 하기
        int cutStageIdx = MapManager.Instance.CurStage.MapIdx;
        Dictionary<EMapDiraction, int> dirRooms = MapManager.Instance.MapList[cutStageIdx];

        foreach(var dirRoom in dirRooms)
        {
            switch (dirRoom.Key)
            {
                case EMapDiraction.Up:
                    upBtn?.gameObject.SetActive(true);
                    break;
                case EMapDiraction.Down:
                    downBtn?.gameObject.SetActive(true);

                    break;
                case EMapDiraction.Left:
                    leftBtn?.gameObject.SetActive(true);

                    break;
                case EMapDiraction.Right:
                    rightBtn?.gameObject.SetActive(true);
                    break;

            }
        }

    }
    public override void CloseUI()
    {
        base.CloseUI();
        returnlBtn?.gameObject.SetActive(false);
        upBtn?.gameObject.SetActive(false);
        downBtn?.gameObject.SetActive(false);
        leftBtn?.gameObject.SetActive(false);
        rightBtn?.gameObject.SetActive(false);
    }
    void MovetoUp()
    {
        MoveToNextStage(EMapDiraction.Up);
    }
    void MovetoDown()
    {
        MoveToNextStage(EMapDiraction.Down);
    }
    void MovetoLeft()
    {
        MoveToNextStage(EMapDiraction.Left);
    }
    void MovetoRight()
    {
        MoveToNextStage(EMapDiraction.Right);
    }
    void MoveToNextStage(EMapDiraction dir)
    {
        if (dir == EMapDiraction.None) return;
        UIManager.Instance.ActiveMenu(false);
        MapManager.Instance.MoveToNextStage(dir);
        CloseUI();
        UIManager.Instance.ActiveMenu(false);
    }


}
