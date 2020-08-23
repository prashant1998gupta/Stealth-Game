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
    private Animator playeranim;
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
        playeranim = player.GetComponent<Animator>();
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


        if (playerHealth.health < 0)
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

        }
    }

}
