using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : BaseUI
{
    [Header("골드 세팅")]
    [SerializeField] RectTransform goldRectTransform;
    [SerializeField] TMPro.TextMeshProUGUI goldText;
    int gold = 0;

    // 위치로 날아가는 골드 아이콘을 위한 RectTransform
    public RectTransform GoldRectTransform { get; }
    public void SetGoldText(int goldAmount)
    {
        goldText.text = goldAmount.ToString();
    }
    public void AddGoldText() // 골드는 플레이어한테 있겠지만, 혹시나 해서
    {
        gold++;
        goldText.text = gold.ToString();
    }
}
