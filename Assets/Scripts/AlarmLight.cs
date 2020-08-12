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

    public  Light lighting;



    void Awake()
    {
        lighting = GetComponent<Light>();
        lighting.intensity = 0f;
        targetIntesity = highIntensity;
    }

    void Update()
    {
        if (alarmOn)
        {
            lighting.intensity = Mathf.Lerp(lighting.intensity, targetIntesity, fadeSpeed * Time.deltaTime);
            checkTargetIntensity();
        }
        else
        {
            lighting.intensity = Mathf.Lerp(lighting.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }
    
    void checkTargetIntensity()
    {
        if(Mathf.Abs(targetIntesity - lighting.intensity) < changeMargin)
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
