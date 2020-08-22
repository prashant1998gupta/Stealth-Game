using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public AudioClip keyGrabSound;

    private GameObject player;
    private PlayerInventrey inventrey;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        inventrey = player.GetComponent<PlayerInventrey>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == (Tags.player))
        {
            AudioSource.PlayClipAtPoint(keyGrabSound, transform.position);
            inventrey.hasKey = true;
            Destroy(gameObject);
        }
        
    }

    
}
