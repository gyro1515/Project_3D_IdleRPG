using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : BaseUI
{
    [Header("골드 세팅")]
    [SerializeField] RectTransform goldRectTransform;
    [SerializeField] TMPro.TextMeshProUGUI goldText;
    [SerializeField] GameObject GoldIconPrefab;
    List<MovingGoldIcon> movingGoldIcons = new List<MovingGoldIcon>(); // 오브젝트 풀링

    int gold = 0;

    // 위치로 날아가는 골드 아이콘을 위한 RectTransform
    public RectTransform GoldRectTransform { get; }
    public void SetGoldText(int goldAmount)
    {
        goldText.text = goldAmount.ToString();
    }
    public void AddMovingGoldIcon(Vector3 startPos)
    {
        for (int i = 0; i < movingGoldIcons.Count; i++)
        {
            if (movingGoldIcons[i].gameObject.activeSelf) continue; // 활성화된 상태면 패스

            movingGoldIcons[i].Init(startPos, goldRectTransform.position);
            return; // 재사용하고 종료
        }
        // 재사용할 오브젝트가 없으면 새로 생성
        GameObject go = Instantiate(GoldIconPrefab, transform);
        MovingGoldIcon movingGoldIcon = go.GetComponent<MovingGoldIcon>();
        if (movingGoldIcon)
        {
            movingGoldIcon?.Init(startPos, goldRectTransform.position);
            movingGoldIcons.Add(movingGoldIcon);
        }
    }
}
