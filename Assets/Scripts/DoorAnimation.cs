using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public bool keyRequired;
    public AudioClip doorSwitchClip;
    public AudioClip accessDeniedClip;

    private Animator anim;
    private HashKeyAnimation hashId;
    private GameObject player;
    private PlayerInventrey inventrey;
    private int count;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hashId = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashKeyAnimation>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        inventrey = player.GetComponent<PlayerInventrey>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == player)
        {
            if(keyRequired)
            {
                if(inventrey.hasKey)
                {
                    count++;
                }
                else
                {
                    GetComponent<AudioSource>().clip = accessDeniedClip;
                    GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                count++;
            }
            
        }
        else if(collider.gameObject.tag ==  Tags.enemy)
        {
            if(collider is CapsuleCollider)
            {
                count++;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject == player || (collider.gameObject.tag == Tags.enemy && collider is CapsuleCollider))
        {
            count = Mathf.Max(0, count - 1);
        }
    }

    void Update()
    {
        anim.SetBool(hashId.openBool, count > 0);

        if (anim.IsInTransition(0) && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().clip = doorSwitchClip;
            GetComponent<AudioSource>().Play();
        }
    }
}
