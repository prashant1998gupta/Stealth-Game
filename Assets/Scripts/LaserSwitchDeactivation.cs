using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitchDeactivation : MonoBehaviour
{
    public GameObject laser;
    public Material unLockMaterial;

    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject == player)
        {
            if(Input.GetButton("Switch"))
            {
                LaserDeactivation();
            }
        }
    }

    void LaserDeactivation()
    {
        laser.SetActive(false);

        MeshRenderer renderer = transform.Find("prop_switchUnit_screen").GetComponent<MeshRenderer>();
        renderer.material = unLockMaterial;

        GetComponent<AudioSource>().Play();
    }

}
