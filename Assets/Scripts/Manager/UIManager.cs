using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMono<UIManager>
{
    public const string UIPrefabPath = "UI/Prefabs/";

    private bool _isCleaning;
    // 껐다 키는 기능만 사용한다면 _uiDictionary가 유용할 거 같지만, 현재는...?
    private Dictionary<string, BaseUI> _uiDictionary = new Dictionary<string, BaseUI>();
    HUD hud;
    Menu menu;
    

    protected override void Awake()
    {
        base.Awake();
        hud = GetUI<HUD>();
        menu = GetUI<Menu>();
    }
    private void Start()
    {
        // 플레이어 정보 가져와서 등록
        GameManager.Instance.Player.Hp.OnValueChange += hud.SetHPBar;
        GameManager.Instance.Player.Mp.OnValueChange += hud.SetMPBar;
        GameManager.Instance.Player.Exp.OnValueChange += hud.SetEXPBar;
        GameManager.Instance.Player.OnLevelUp += hud.SetLVText;
        GameManager.Instance.Player.OnGoldChanged += hud.SetGoldText;
        // 이벤트 구독하고 플레이어 상태에 맞게 UI초기화
        GameManager.Instance.Player.UIInit();
        hud.SetStageText(11);
    }


    // ================================
    // UI 관리
    // ================================
    public void OpenUI<T>() where T : BaseUI
    {
        var ui = GetUI<T>();
        ui?.OpenUI();
    }

    public void CloseUI<T>() where T : BaseUI
    {
        if (IsExistUI<T>())
        {
            var ui = GetUI<T>();
            ui?.CloseUI();
        }
    }

    public T GetUI<T>() where T : BaseUI
    {
        if (_isCleaning) return null;

        string uiName = GetUIName<T>();

        BaseUI ui;
        if (IsExistUI<T>())
            ui = _uiDictionary[uiName];
        else
            ui = CreateUI<T>();

        return ui as T;
    }

    private T CreateUI<T>() where T : BaseUI
    {
        if (_isCleaning) return null;

        string uiName = GetUIName<T>();
        if (_uiDictionary.TryGetValue(uiName, out var prevUi) && prevUi != null)
        {
            Destroy(prevUi.gameObject);
            _uiDictionary.Remove(uiName);
        }

        // 1. 프리팹 로드
        string path = GetPath<T>();
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab == null)
        {
            Debug.LogError($"[UIManager] Prefab not found: {path}");
            return null;
        }

        // 2. 인스턴스 생성
        GameObject go = Instantiate(prefab, gameObject.transform);

        // 3. 컴포넌트 획득
        T ui = go.GetComponent<T>();
        if (ui == null)
        {
            Debug.LogError($"[UIManager] Prefab has no component : {uiName}");
            Destroy(go);
            return null;
        }

        // 4. Dictionary 등록
        _uiDictionary[uiName] = ui;

        return ui;
    }

    public bool IsExistUI<T>() where T : BaseUI
    {
        string uiName = GetUIName<T>();
        return _uiDictionary.TryGetValue(uiName, out var ui) && ui != null;
    }


    // ================================
    // path 헬퍼
    // ================================
    private string GetPath<T>() where T : BaseUI
    {
        return UIPrefabPath + GetUIName<T>();
    }

    private string GetUIName<T>() where T : BaseUI  
    {
        return typeof(T).Name;
    }


    // ================================
    // 리소스 정리
    // ================================
    private void OnSceneUnloaded(Scene scene)
    {
        CleanAllUIs();
        StartCoroutine(CoUnloadUnusedAssets());
    }

    private void CleanAllUIs()
    {
        if (_isCleaning) return;
        _isCleaning = true;

        try
        {
            foreach (var ui in _uiDictionary.Values)
            {
                if (ui == null) continue;
                // Close 프로세스 추가 가능
                Destroy(ui.gameObject);
            }
            _uiDictionary.Clear();
        }
        finally
        {
            _isCleaning = false;
        }
    }

    // UI 뿐만 아니라 전체 오브젝트 관리 시스템측면에서도 있으면 좋음
    private IEnumerator CoUnloadUnusedAssets()
    {
        yield return Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
