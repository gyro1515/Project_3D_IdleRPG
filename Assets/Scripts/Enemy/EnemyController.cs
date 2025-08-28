using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    Enemy enemy;
    public event Action<Vector3> OnDropGold;
    private EnemyStateMachine stateMachine;
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
        stateMachine = new EnemyStateMachine(enemy);
    }
    private void Start()
    {
        stateMachine.Target = GameManager.Instance.Player;
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    private void Update()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    protected override void OnDIe()
    {
        base.OnDIe();
        // 죽으면 골드 드랍
        StartCoroutine(DropGold(enemy.DropGold()));
    }
    IEnumerator DropGold(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // 골드 드랍 = 화면에 골드 이미지 생성하고 골드 UI로 날아가기
            OnDropGold?.Invoke(gameObject.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
