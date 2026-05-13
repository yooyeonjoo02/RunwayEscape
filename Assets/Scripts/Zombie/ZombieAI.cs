using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public enum ZombieState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public ZombieState currentState = ZombieState.Idle;

    public Transform player;

    public float detectRange = 10f;
    public float attackRange = 2f;
    public float patrolRange = 5f;
    public float moveSpeed = 3f;

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private Vector3 patrolTarget;

    private float idleTimer = 0f;
    public float idleTime = 2f;

    public int hp = 100;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;

        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
        }

        agent.speed = moveSpeed;
        SetNewPatrolTarget();
    }

    void Update()
    {
        if (hp <= 0)
        {
            ChangeState(ZombieState.Dead);
        }

        switch (currentState)
        {
            case ZombieState.Idle:
                Idle();
                break;

            case ZombieState.Patrol:
                Patrol();
                break;

            case ZombieState.Chase:
                Chase();
                break;

            case ZombieState.Attack:
                Attack();
                break;

            case ZombieState.Dead:
                Dead();
                break;
        }
    }

    void Idle()
    {
        agent.isStopped = true;

        idleTimer += Time.deltaTime;

        if (CanSeePlayer())
        {
            ChangeState(ZombieState.Chase);
            return;
        }

        if (idleTimer >= idleTime)
        {
            idleTimer = 0f;
            ChangeState(ZombieState.Patrol);
        }
    }

    void Patrol()
    {
        agent.isStopped = false;
        agent.SetDestination(patrolTarget);

        if (CanSeePlayer())
        {
            ChangeState(ZombieState.Chase);
            return;
        }

        if (Vector3.Distance(transform.position, patrolTarget) < 1f)
        {
            SetNewPatrolTarget();
            ChangeState(ZombieState.Idle);
        }
    }

    void Chase()
    {
        if (player == null) return;

        agent.isStopped = false;
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            ChangeState(ZombieState.Attack);
        }
        else if (distance > detectRange * 1.5f)
        {
            ChangeState(ZombieState.Patrol);
        }
    }

    void Attack()
    {
        if (player == null) return;

        agent.isStopped = true;
        transform.LookAt(player);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            ChangeState(ZombieState.Chase);
        }

        Debug.Log("좀비가 공격 중!");
    }

    void Dead()
    {
        agent.isStopped = true;
        Debug.Log("좀비 사망");
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= detectRange;
    }

    void SetNewPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += startPosition;
        randomDirection.y = transform.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
        }
    }

    void ChangeState(ZombieState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        Debug.Log("좀비 상태 변경: " + currentState);
    }
}