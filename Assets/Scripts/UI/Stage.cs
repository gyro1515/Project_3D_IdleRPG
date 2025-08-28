using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : BaseUI
{
    [Header("스테이지 선택 세팅")]
    [SerializeField] Button returnlBtn;
    private void Awake()
    {
        returnlBtn?.onClick.AddListener(CloseUI);
        CloseUI();

    }
}
