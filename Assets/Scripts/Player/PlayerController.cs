using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    Player player;
    private PlayerStateMachine stateMachine;
    private NavMeshAgent pathFinder; // 경로계산 AI 에이전트
    // 테스트
    [SerializeField] Vector3 testPos;
    public bool IsNavMesh { get; set; }

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        stateMachine = new PlayerStateMachine(player);
        pathFinder = GetComponent<NavMeshAgent>();
        pathFinder.speed = player.PlayerData.GroundData.BaseSpeed;
        pathFinder.updateRotation = true;
    }
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            pathFinder.SetDestination(testPos);
            isNavMesh = !isNavMesh;
        }*/
        if (IsNavMesh)
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            Vector3 dir = pathFinder.velocity;
            dir.y = 0f;
            dir = dir.normalized;
            //gameObject.transform.rotation = Quaternion.LookRotation(dir);
            return;
        }
        stateMachine.Update();
        
    }
    private void FixedUpdate()
    {
        if (IsNavMesh) return;
        stateMachine.PhysicsUpdate();
    }
    public void AddExp(int amount)
    {
        if (!player) return;
        if(player.Exp.CurrentValue + amount >= player.Exp.MaxValue) // 레벨업
        {
            player.Level++;
            float overflow = (player.Exp.CurrentValue + amount) - player.Exp.MaxValue;
            player.Exp.CurrentValue = overflow;
            player.Exp.MaxValue *= 1.2f; // 최대 경험치 20% 증가
        }
        else
        {
            player.Exp.CurrentValue += amount;
        }
    }
    protected override void OnDIe()
    {
        base.OnDIe();
        // 죽으면 게임오버
    }
    public void MoveToPos(Vector3 pos)
    {
        IsNavMesh = true;
        pathFinder.SetDestination(pos);
    }
}
