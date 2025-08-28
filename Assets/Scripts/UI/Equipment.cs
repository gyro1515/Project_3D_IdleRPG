using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : BaseInventory
{
    [Header("장착 창 세팅")]
    [SerializeField] Button returnlBtn;
    [SerializeField] Button equipBtn;
    [SerializeField] Button unEquipBtn;
    [SerializeField] Button useBtn;


    protected override void Awake()
    {
        // 인벤토리 세팅
        base.Awake();
        returnlBtn?.onClick.AddListener(CloseUI);
        CloseUI();
    }
    protected override void Start()
    {
        base.Start();
    }
}
