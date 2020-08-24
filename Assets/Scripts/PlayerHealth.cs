using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float resetAfterDeathTime = 5f;
    public AudioClip clip;

    private Animator anim;
    private PlayerMovement playerMovement;
    private HashKeyAnimation hashId;
    private FaderInOut fader;
    private LastPlayerSighting playerSighting;
    private float timer;
    private bool playerDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        hashId = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashKeyAnimation>();
        fader = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<FaderInOut>();
        playerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

    }

    void Update()
    {
        if(health <= 0f)
        {
            if(!playerDead)
            {
                PlayerDying();
            }
            else
            {
                PlayerDead();
                LavelReset();
            }
        }
    }
    void PlayerDying()
    {
        playerDead = true;
        anim.SetBool(hashId.deadBool, true);
        AudioSource.PlayClipAtPoint(clip, transform.position); 
    }

    void PlayerDead()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hashId.dyingState)
        {
            anim.SetBool(hashId.deadBool, false);
        }

        anim.SetFloat(hashId.speedFloat, 0f);
        playerMovement.enabled = false;
        playerSighting.position = playerSighting.resetPosition;
        GetComponent<AudioSource>().Stop();
    }

    void LavelReset()
    {
        timer += Time.deltaTime;

        if(timer > resetAfterDeathTime)
        {
            fader.EndScene();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
