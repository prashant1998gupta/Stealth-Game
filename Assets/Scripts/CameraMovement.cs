using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float smooth = 1.5f;

    private Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }

   /* void Start()
    {
        Debug.Log(relCameraPos);
        Debug.Log(transform.position);
        Debug.Log(player.position);

    }*/

    void FixedUpdate()
    {
        Vector3 standardPos = player.position + relCameraPos;
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
        /*Vector3 smoothPosition = Vector3.Lerp(transform.position, standardPos, smooth);
        transform.position = smoothPosition;*/
        Vector3[] checkPoint = new Vector3[5];
        checkPoint[0] = Vector3.Lerp(standardPos, abovePos, .25f);
        checkPoint[1] = Vector3.Lerp(standardPos, abovePos, .5f);
        checkPoint[2] = Vector3.Lerp(standardPos, abovePos, .75f);
        checkPoint[3] = standardPos;
        checkPoint[4] = abovePos;

        for (int i = 0; i < checkPoint.Length; i++)
        {
            if(VeiwingPosCheck(checkPoint[i]))
            {
                break;
            }

        }

        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
        SmoothLookAt();
    }

    bool VeiwingPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            if(hit.transform != player)
            {
                return false;
            }
        }
        newPos = checkPos;
        return true;
    }

    void SmoothLookAt()
    {
        Vector3 relPlayerPos = player.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }

}
