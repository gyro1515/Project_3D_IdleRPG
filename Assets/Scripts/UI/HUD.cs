using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : BaseUI
{
    [Header("HUD 세팅")]
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Image mpBar;
    [SerializeField] TextMeshProUGUI mpText;
    [SerializeField] Image expBar;
    [SerializeField] TextMeshProUGUI epText;
    [SerializeField] TextMeshProUGUI lvText;
    [SerializeField] TextMeshProUGUI stageText;
    
    Gold gold;
    private void Awake()
    {
        gold = GetComponentInChildren<Gold>();
    }
    public void Init()
    {

    }

    public void SetHPBar(float curValue, float maxValue)
    {
        hpBar.fillAmount = curValue / maxValue;
        hpText.text = $"{curValue} / {maxValue}";
    }
    public void SetMPBar(float curValue, float maxValue)
    {
        mpBar.fillAmount = curValue / maxValue;
        mpText.text = $"{curValue} / {maxValue}";
    }
    public void SetEXPBar(float curValue, float maxValue)
    {
        expBar.fillAmount = curValue / maxValue;
        epText.text = $"{curValue} / {maxValue}";
    }
    public void SetLVText(int lv)
    {
        lvText.text = $"Lv. {lv}";
    }
    public void SetStageText(int stage)
    {
        stageText.text = $"스테이지: {stage}";
    }
    public void SetGoldText(int goldAmount)
    {
        gold?.SetGoldText(goldAmount);
    }
}
