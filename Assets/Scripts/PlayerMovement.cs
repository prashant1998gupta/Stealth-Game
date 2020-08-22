using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip audioClip;
    public float turnSmoothing = 15f;
    public float speedDamping = .1f;


    private Animator anim;
    private HashKeyAnimation hashID;
    private AudioSource audioSource;
    private Rigidbody rigidbodyPlayer;


    void Awake()
    {
        anim = GetComponent<Animator>();
        hashID = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashKeyAnimation>();
        audioSource = GetComponent<AudioSource>();
        rigidbodyPlayer = GetComponent<Rigidbody>();
        anim.SetLayerWeight(1, 1f);
        
    }

    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hashID.shoutingBool, shout);
        AudioManagement(shout);

    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");

        MovementManagement(h, v, sneak);

    }

    void MovementManagement(float horizonatal , float vertical , bool sneak)
    {
        anim.SetBool(hashID.sneakingBool, sneak);

        if(horizonatal != 0 || vertical != 0)
        {
            Rotating(horizonatal, vertical);
            anim.SetFloat(hashID.speedFloat, 5.5f, speedDamping, Time.deltaTime);
        }
        else
        {
            anim.SetFloat(hashID.speedFloat, 0f);
        }
    }

    void Rotating(float horizontal , float vertical)
    {
        Vector3 targetDirection = new Vector3(horizontal, 0, vertical);
        Quaternion trrgetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rigidbodyPlayer.rotation, trrgetRotation, turnSmoothing * Time.deltaTime);
        rigidbodyPlayer.MoveRotation(newRotation);

    }

    void AudioManagement(bool shout)
    {
        if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hashID.locomotionState)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }

        if(shout)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }
}
