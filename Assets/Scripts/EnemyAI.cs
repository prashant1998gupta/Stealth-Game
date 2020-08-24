using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float patorlSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    private EnemySight enemySight;
    private NavMeshAgent agent;
    private Transform player;
    private PlayerHealth playerHealth;
    private LastPlayerSighting playerSighting;
    private float chaseTimer;
    private float patroltimer;
    private int wayPointIndex;

    void Awake()
    {
        enemySight = GetComponent<EnemySight>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void Update()
    {
        if (enemySight.playerIsSight && playerHealth.health > 0f)
            shooting();
        else if (enemySight.personalLastSight != playerSighting.resetPosition && playerHealth.health > 0f)
            Chasing();
        else
            Patrolling();
       
    }

    void shooting()
    {
        agent.isStopped = true;
    }

    void Chasing()
    {
        Vector3 sightingDeltaPos = enemySight.personalLastSight - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4)
        {
            agent.destination = enemySight.personalLastSight;
        }

        agent.speed = chaseSpeed;
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer > chaseWaitTime)
            {
                playerSighting.position = playerSighting.resetPosition;
                enemySight.personalLastSight = playerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
    }


    void Patrolling()
    {
        agent.speed = patorlSpeed;
        if (agent.destination == playerSighting.resetPosition || agent.remainingDistance < agent.stoppingDistance)
        {
            patroltimer += Time.deltaTime;
            if (patroltimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                patroltimer = 0f;
            }
        }
        else
            patroltimer = 0f;

        agent.destination = patrolWayPoints[wayPointIndex].position;
        

    }
}
