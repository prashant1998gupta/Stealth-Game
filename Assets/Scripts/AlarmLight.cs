using System;
// using Object = System.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    public float fadeSpeed = 2f;
    public float lowIntensity = 0.5f;
    public float highIntensity = 2f;

    public float changeMargin = 0.2f;

    public bool alarmOn;

    private float targetIntesity;
    
        
    void Awake()
    {
        GetComponent<Light>().intensity = 0f;

        targetIntesity = highIntensity;
        Transform myTras = null;

        myTras = GetComponent<Transform>();


       Object  dsd = GetComponent<Transform>().GetComponent<Light>().
;
    }

    void Update()
    {
        if (alarmOn)
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, targetIntesity, fadeSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }
    
    void checkTargetIntensity()
    {
        if(Mathf.Abs(targetIntesity - GetComponent<Light>().intensity) < changeMargin)
        {
            if(targetIntesity == highIntensity)
            {
                targetIntesity = lowIntensity;
            }
            else
            {
                targetIntesity = highIntensity;
            }
        }
    }


   

}
