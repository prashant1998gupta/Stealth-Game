using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    public float deadZone = 5f;

    private Transform player;
    private EnemySight enemySight;
    private AnimatorSetup animatorSetup;
    private Animator anim;
    private NavMeshAgent agent;
    private HashKeyAnimation hashId;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        enemySight = GetComponent<EnemySight>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hashId = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashKeyAnimation>();

        agent.updateRotation = false;

        animatorSetup = new AnimatorSetup(anim, hashId);

        anim.SetLayerWeight(1, 1f);
        anim.SetLayerWeight(2, 1f);

        deadZone *= Mathf.Deg2Rad;
    }

    /*void Start()
    {
        Debug.Log("this is Enemy Animator Start");
    }*/

    void Update()
    {
        NavAgentAnimSetUp();
    }

    void OnAnimatorMove()
    {
        agent.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }
    void NavAgentAnimSetUp()
    {
        float speed;
        float angle;

        if(enemySight.playerIsSight)
        {
            speed = 0f;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;

            angle = FindAngle(transform.forward, agent.desiredVelocity, transform.up);

            if(Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + agent.desiredVelocity);
                angle = 0f;
            }
        }

        animatorSetup.SetUp(speed, angle);
    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if(toVector == Vector3.zero)
        {
            return 0f;
        }

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;

        return angle;
    }


}
