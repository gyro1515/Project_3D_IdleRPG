using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] GameObject playerPrefab;
    public Player Player { get; private set; }

    // 테스트 용***********
    [Header("적 소환 테스트")]
    public List<GameObject> enemyPrefabs;
    public int spawnCount = 5;
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
    // ********************

    protected override void Awake()
    {
        base.Awake();
        Player = Instantiate(playerPrefab).GetComponent<Player>();
        // 테스트 용 적 소환
        for (int i = 0; i < spawnCount; i++)
        {
            for (int j = 0; j < enemyPrefabs.Count; j++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-12.5f, 12.5f), 0, Random.Range(-12.5f, 12.5f));
                Vector3 rot = new Vector3(0, Random.Range(0f, 360f), 0);
                GameObject enemyObj = Instantiate(enemyPrefabs[j], spawnPosition, Quaternion.Euler(rot));
                enemyObj.transform.parent = transform;
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemies.Add(enemy);
                }
            }
        }
    }
    private void Start()
    {
        // 적 체력 바 세팅
        for(int i = 0; i < enemies.Count;i++)
        {
            UIManager.Instance.AddEnemyHpBar(enemies[i]);
            enemies[i].GetComponent<EnemyController>().OnDropGold += UIManager.Instance.AddMovingGoldIcon;
        }
    }
    public BaseCharacter GetEnemyTarget()
    {
        BaseCharacter target = null;
        float minDist = float.MaxValue;
        for (int i = 0; i < enemies.Count; i++)
        {

            if (enemies[i].IsDIe) continue;
            if (enemies[i] && minDist > (enemies[i].gameObject.transform.position - Player.gameObject.transform.position).sqrMagnitude)
            {
                minDist = (enemies[i].gameObject.transform.position - Player.gameObject.transform.position).sqrMagnitude;
                target = enemies[i];
            }
        }
        if(target == null)
        {
            // 타겟이 더이상 없다면
            UIManager.Instance.OpenMenu();
        }
        return target;
    }

}
