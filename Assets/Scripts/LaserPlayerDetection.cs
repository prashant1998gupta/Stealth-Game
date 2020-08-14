using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour
{
    public GameObject player;
    public LastPlayerSighting lastPlayerSighting;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void OnTriggerStay(Collider collider)
    {
        if(GetComponent<MeshRenderer>())
        {
            if(collider.gameObject == player)
            {
                lastPlayerSighting.position = collider.transform.position;
            }
        }
    }
}
