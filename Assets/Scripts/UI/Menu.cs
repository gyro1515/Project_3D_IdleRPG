using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : BaseUI
{
    [Header("메뉴 세팅")]
    [SerializeField] Button openInvenBtn;
    [SerializeField] Button openShopBtn;
    [SerializeField] Button openStageSelBtn;
    [SerializeField] Equipment equipment;
    [SerializeField] Shop shop;
    [SerializeField] Stage stageSelect;
    
    private void Awake()
    {
        openInvenBtn?.onClick.AddListener(equipment.OpenUI);
        openShopBtn?.onClick.AddListener(shop.OpenUI);
        openStageSelBtn?.onClick.AddListener(() => stageSelect.OpenUI()); // 람다식도 된다면 활용해보기
    }
}
