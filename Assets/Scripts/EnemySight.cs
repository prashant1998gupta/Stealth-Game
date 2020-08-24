using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerIsSight;
    public Vector3 personalLastSight;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private LastPlayerSighting playerSighting;
    private GameObject player;
    private Animator playerAnim;
    private HashKeyAnimation hashId;
    private PlayerHealth playerHealth;
    private Vector3 previousSighting;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        playerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        hashId = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashKeyAnimation>();

        personalLastSight = playerSighting.resetPosition;
        previousSighting = playerSighting.resetPosition;
    }

    void Update()
    {
        if(playerSighting.position != previousSighting)
        {
            personalLastSight = playerSighting.position;
        }
        previousSighting = playerSighting.position;


        if (playerHealth.health > 0)
            anim.SetBool(hashId.playerInSightBool, playerIsSight);
        else
            anim.SetBool(hashId.playerInSightBool, false);
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject == player)
        {
            playerIsSight = false;

            Vector3 direction = collider.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < fieldOfViewAngle*0.5f)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position + transform.up , direction.normalized , out hit , col.radius))
                {
                    if(hit.collider.gameObject == player)
                    {
                        playerIsSight = true;
                        playerSighting.position = player.transform.position;
                    }
                }
            }

            int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;

            if(playerLayerZeroStateHash == hashId.locomotionState || playerLayerOneStateHash == hashId.shoutState)
            {
                if(CalculatePathLenght(transform.position) <= col.radius)
                {
                    personalLastSight = player.transform.position;
                }
            }

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject == player)
        {
            playerIsSight = false;
        }
    }

    float CalculatePathLenght(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if(nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLenght = 0f;

        for (int i = 0; i < allWayPoints.Length-1; i++)
        {
            pathLenght += Vector3.Distance(allWayPoints[i] , allWayPoints[i+1]);
        }

        return pathLenght;
    }
}
